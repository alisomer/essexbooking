/*
 * Code From http://madskristensen.net/post/A-realtime-currency-exchange-class-in-C.aspx
 * 
 * Author: Mads Kristensen
 * 
 * Example Usage
 * 
 * double fromUSDtoEUR = Currency.Exchange("USD", "EUR", 12.75 );
 * double rateFromUSDtoNOK = Currency.GetRate("NOK", "USD");
 */

#region

using System;
using System.IO;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Collections.Generic;
using System.Timers;
using System.Net;

#endregion

/// <summary>
/// Maintains a list of currencies and exchange rates
/// and keeps it up to date. 
/// </summary>
public static class Currency
{
  static Currency()
  {
    SetFilename();
    SetTimer();
    Download();
    DownloadComplete += delegate { ProcessXml(); };
  }

  #region Private fields

  private static Dictionary<string, double> _Currencies = new Dictionary<string, double>();
  private static DateTime _LastUpdated;
  private static DateTime _LastModified;
  private static Timer _Timer = null;
  private static string _Url = "http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml";
  private static string _Filename;
  private static int _UpdateInterval = 1 * (60 * 60 * 1000);

  #endregion

  #region Properties

  /// <summary>
  /// Gets the date of the current exchange rates.
  /// </summary>
  public static DateTime LastUpdated
  {
    get { return _LastUpdated; }
  }

  /// <summary>
  /// Gets the absolute path to the XML document.
  /// </summary>
  public static string Filename
  {
    get { return _Filename; }
  }

  #endregion

  #region Public methods

  /// <summary>
  /// Converts an amount of one currency to another.
  /// </summary>
  public static double Exchange(string fromCurrency, string toCurrency, double value)
  {
    double from = _Currencies[fromCurrency.ToUpperInvariant()];
    double to = _Currencies[toCurrency.ToUpperInvariant()];
    return value * (to / from);
  }

  /// <summary>
  /// Get the exchange rate for one single currency.
  /// The rate is relative to the EURO.
  /// </summary>
  /// <param name="currency">The three letter ISO code.</param>
  /// <returns>The rate of the currency relative to the EURO.</returns>
  public static double GetRate(string currency)
  {
    return _Currencies[currency.ToUpperInvariant()];
  }

  /// <summary>
  /// Get the exchange rate for one single currency, based
  /// on another currency.
  /// </summary>
  /// <param name="currency">The currency rate you want.</param>
  /// <param name="baseCurrency">The currency to base it on.</param>
  /// <returns>The rate of the currency relative to the baseCurrency.</returns>
  public static double GetRate(string currency, string baseCurrency)
  {
    double from = _Currencies[currency.ToUpperInvariant()];
    double to = _Currencies[baseCurrency.ToUpperInvariant()];
    return to / from;
  }

  /// <summary>
  /// Gets a list of all the latest exchange rates.
  /// </summary>
  public static Dictionary<string, double> GetAllRates()
  {
    return _Currencies;
  }

  /// <summary>
  /// Download the lates exchange rates and
  /// parse the downloaded XML file.
  /// </summary>
  public static void Update()
  {
    Download();
  }

  #endregion

  #region Private methods

  /// <summary>
  /// Sets the filename from the appsettings 
  /// whether it is relative or absolute.
  /// </summary>
  private static void SetFilename()
  {
    string path = ConfigurationManager.AppSettings.Get("Currency.Filename");
    if (!path.Contains("\\"))
      _Filename = HttpContext.Current.Server.MapPath(path);
    else
      _Filename = path;
  }

  /// <summary>
  /// Instantiates and starts the timer.
  /// </summary>
  private static void SetTimer()
  {
    _Timer = new Timer();
    _Timer.Interval = _UpdateInterval;
    _Timer.Enabled = true;
    _Timer.Elapsed += new ElapsedEventHandler(_Timer_Elapsed);
  }

  /// <summary>
  /// Update the exchange rates every time the Timer elapses.
  /// </summary>
  static void _Timer_Elapsed(object sender, ElapsedEventArgs e)
  {
    Download();
  }

  /// <summary>
  /// Parse the downloaded XML file.
  /// </summary>
  private static void ProcessXml()
  {
    if (!File.Exists(_Filename))
    {
      throw new Exception("The currencies haven't been downloaded.");
    }

    Dictionary<string, double> currencies = new Dictionary<string, double>();
    currencies.Add("EUR", 1.0);

    using (FileStream fs = new FileStream(_Filename, FileMode.Open, FileAccess.Read, FileShare.Read))
    {
      using (XmlTextReader xmlReader = new XmlTextReader(fs))
      {
        while (xmlReader.Read())
        {
          for (int i = 0; i < xmlReader.AttributeCount; i++)
          {
            AddCurrency(currencies, xmlReader);
            xmlReader.MoveToNextAttribute();
          }
        }
      }
    }

    _Currencies = currencies;
    OnUpdateComplete();
  }

  /// <summary>
  /// Reads through the XML file and adds the new
  /// exchange rates to the list.
  /// </summary>
  private static void AddCurrency(Dictionary<string, double> currencies, XmlTextReader xmlReader)
  {
    if (xmlReader.Name == "Cube")
    {
      if (xmlReader.AttributeCount == 1)
      {
        xmlReader.MoveToAttribute("time");
        _LastUpdated = DateTime.Parse(xmlReader.Value);
      }

      if (xmlReader.AttributeCount == 2)
      {
        xmlReader.MoveToAttribute("currency");
        string name = xmlReader.Value;

        xmlReader.MoveToAttribute("rate");
        //double rate = double.Parse(xmlReader.Value.Replace(".", ","));
        double rate = double.Parse(xmlReader.Value);

        currencies.Add(name, rate);
      }
    }
  }

  /// <summary>
  /// Downloads the latest exchange rates from ECB.
  /// </summary>
  private static void Download()
  {
    try
    {
      _Timer.Stop();
      HttpWebRequest request = WebRequest.Create(_Url) as HttpWebRequest;
      using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
      {
        if (response.StatusCode == HttpStatusCode.OK && response.LastModified != _LastModified)
        {
          using (StreamWriter writer = new StreamWriter(_Filename, false))
          {
            StreamReader reader = new StreamReader(response.GetResponseStream());
            writer.Write(reader.ReadToEnd());
            reader.Close();
          }
          _LastModified = response.LastModified;
        }
      }

      OnDownloadComplete();
      _Timer.Interval = _UpdateInterval;
    }
    catch (Exception)
    {
      // If an error occurs, try again in 10 minuttes.
      _Timer.Interval = 10 * (1000 * 60);
    }
    finally
    {
      _Timer.Start();
    }
  }

  #endregion

  #region Events

  /// <summary>
  /// Occurs when the currencies has been downloadee
  /// </summary>
  private static event EventHandler<EventArgs> DownloadComplete;
  private static void OnDownloadComplete()
  {
    if (DownloadComplete != null)
    {
      DownloadComplete(null, new EventArgs());
    }
  }

  /// <summary>
  /// Occurs when the currencies has been updated
  /// </summary>
  public static event EventHandler<EventArgs> UpdateComplete;
  private static void OnUpdateComplete()
  {
    if (UpdateComplete != null)
    {
      UpdateComplete(null, new EventArgs());
    }
  }


  #endregion

}
