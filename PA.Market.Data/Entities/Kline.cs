using System.Data;
using System.Data.SqlClient;
using PropertyValidation;
using System;
using System.Collections.Generic;
using Automation.TableProvider;
using Binance.Shared.Models;

namespace PA.StockMarket.Data
{

	#region Kline
	/// <summary>
	/// This object represents the properties and methods of a Kline.
	/// </summary>

	public partial class Kline : System.ComponentModel.INotifyPropertyChanged, ICloneable
	{
		protected long _iD;
		protected long _symbolID;
		protected string _interval;
		protected DateTime _openUTCTime;
		protected double _openPrice;
		protected double _closePrice;
		protected double _highPrice;
		protected double _lowPrice;
		protected DateTime _closeUTCTime;
		protected double _volume;
		protected double _quoteAssetVolume;
		protected long _numberOfTrades;
		protected double _rSI;
		protected double _aTR;
		protected double _eMA;
		protected double _sMA;
		protected double _rMA;

		public Kline()
		{
		}

		#region Public Properties

		public long ID
		{
			get { return _iD; }
			set
			{
				_iD = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ID"));
			}
		}

		public long SymbolID
		{
			get { return _symbolID; }
			set
			{
				_symbolID = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SymbolID"));
			}
		}

		public string Interval
		{
			get { return _interval; }
			set
			{
				_interval = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Interval"));
			}
		}

		public DateTime OpenUTCTime
		{
			get { return _openUTCTime; }
			set
			{
				_openUTCTime = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("OpenUTCTime"));
			}
		}

		public double OpenPrice
		{
			get { return _openPrice; }
			set
			{
				_openPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("OpenPrice"));
			}
		}

		public double ClosePrice
		{
			get { return _closePrice; }
			set
			{
				_closePrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ClosePrice"));
			}
		}

		public double HighPrice
		{
			get { return _highPrice; }
			set
			{
				_highPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("HighPrice"));
			}
		}

		public double LowPrice
		{
			get { return _lowPrice; }
			set
			{
				_lowPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("LowPrice"));
			}
		}

		public DateTime CloseUTCTime
		{
			get { return _closeUTCTime; }
			set
			{
				_closeUTCTime = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("CloseUTCTime"));
			}
		}

		public double Volume
		{
			get { return _volume; }
			set
			{
				_volume = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Volume"));
			}
		}

		public double QuoteAssetVolume
		{
			get { return _quoteAssetVolume; }
			set
			{
				_quoteAssetVolume = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("QuoteAssetVolume"));
			}
		}

		public long NumberOfTrades
		{
			get { return _numberOfTrades; }
			set
			{
				_numberOfTrades = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("NumberOfTrades"));
			}
		}

		public double RSI
		{
			get { return _rSI; }
			set
			{
				if (double.IsNaN(value))
					value = 0;
				_rSI = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("RSI"));
			}
		}

		public double ATR
		{
			get { return _aTR; }
			set
			{
				if (double.IsNaN(value))
					value = 0;
				_aTR = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ATR"));
			}
		}

		public double EMA
		{
			get { return _eMA; }
			set
			{
				if (double.IsNaN(value))
					value = 0;
				_eMA = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("EMA"));
			}
		}

		public double SMA
		{
			get { return _sMA; }
			set
			{
				if (double.IsNaN(value))
					value = 0;
				_sMA = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SMA"));
			}
		}

		public double RMA
		{
			get { return _rMA; }
			set
			{
				if (double.IsNaN(value))
					value = 0;
				_rMA = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("RMA"));
			}
		}

		#endregion

		#region Public Methods

		public List<ValidationResult> Validate()
		{
			List<ValidationResult> errors = new List<ValidationResult>();

			return errors;
		}

		public List<ValidationResult> DeepValidate()
		{
			List<ValidationResult> errors = new List<ValidationResult>();

			return errors;
		}

		public Kline TypedClone()
		{
			return (Kline)((ICloneable)this).Clone();
		}

		public void CopyData(Kline value)
		{
			this.ID = value.ID;
			this.SymbolID = value.SymbolID;
			this.Interval = value.Interval;
			this.OpenUTCTime = value.OpenUTCTime;
			this.OpenPrice = value.OpenPrice;
			this.ClosePrice = value.ClosePrice;
			this.HighPrice = value.HighPrice;
			this.LowPrice = value.LowPrice;
			this.CloseUTCTime = value.CloseUTCTime;
			this.Volume = value.Volume;
			this.QuoteAssetVolume = value.QuoteAssetVolume;
			this.NumberOfTrades = value.NumberOfTrades;
			this.RSI = value.RSI;
			this.ATR = value.ATR;
			this.EMA = value.EMA;
			this.SMA = value.SMA;
			this.RMA = value.RMA;
		}
		public void CopyData(Candlestick value)
		{
			this.Interval = value.Interval;
			this.OpenUTCTime = value.OpenTime;
			this.OpenPrice = value.OpenPrice;
			this.ClosePrice = value.ClosePrice;
			this.HighPrice = value.HighPrice;
			this.LowPrice = value.LowPrice;
			this.CloseUTCTime = value.CloseTime;
			this.Volume = value.Volume;
			this.QuoteAssetVolume = value.QuoteAssetVolume;
			this.NumberOfTrades = value.NumberOfTrades;
			this.RSI = value.RSI;
			this.ATR = value.ATR;
			this.EMA = value.EMA;
			this.SMA = value.SMA;
			this.RMA = value.RMA;
		}
		public Candlestick CopyTo()
		{
			Candlestick value = new Candlestick();
			value.Interval = this.Interval;
			value.OpenTime  = this.OpenUTCTime;
			value.OpenPrice  = this.OpenPrice;
			value.ClosePrice  = this.ClosePrice;
			value.HighPrice  = this.HighPrice;
			value.LowPrice  = this.LowPrice;
			value.CloseTime  = this.CloseUTCTime;
			value.Volume  = this.Volume;
			value.QuoteAssetVolume  = this.QuoteAssetVolume;
			value.NumberOfTrades  = this.NumberOfTrades;
			value.RSI = this.RSI;
			value.ATR = this.ATR;
			value.EMA = this.EMA;
			value.SMA = this.SMA;
			value.RMA = this.RMA;
			return value;
		}

		#endregion Public Methods

		#region Private Methods

		#endregion Private Methods	

		#region INotifyPropertyChanged Members

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region ICloneable Members

		object ICloneable.Clone()
		{
			Kline obj = new Kline();

			obj.ID = this.ID;
			obj.SymbolID = this.SymbolID;
			obj.Interval = this.Interval;
			obj.OpenUTCTime = this.OpenUTCTime;
			obj.OpenPrice = this.OpenPrice;
			obj.ClosePrice = this.ClosePrice;
			obj.HighPrice = this.HighPrice;
			obj.LowPrice = this.LowPrice;
			obj.CloseUTCTime = this.CloseUTCTime;
			obj.Volume = this.Volume;
			obj.QuoteAssetVolume = this.QuoteAssetVolume;
			obj.NumberOfTrades = this.NumberOfTrades;
			obj.RSI = this.RSI;
			obj.ATR = this.ATR;
			obj.EMA = this.EMA;
			obj.SMA = this.SMA;
			obj.RMA = this.RMA;

			return obj;
		}

		#endregion
	}
	#endregion

	//***********************************************************************************************

	#region Kline Data Provider
	namespace DataAccess
	{

		public static class KlineDataProvider
		{
			#region Insert Methods

			public static long Insert(Kline value)
			{
				SqlCommand command = new SqlCommand("SP_InsertKline", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", value.SymbolID);
				command.Parameters.AddWithValue("@Interval", value.Interval);
				command.Parameters.AddWithValue("@OpenUTCTime", value.OpenUTCTime);
				command.Parameters.AddWithValue("@OpenPrice", value.OpenPrice);
				command.Parameters.AddWithValue("@ClosePrice", value.ClosePrice);
				command.Parameters.AddWithValue("@HighPrice", value.HighPrice);
				command.Parameters.AddWithValue("@LowPrice", value.LowPrice);
				command.Parameters.AddWithValue("@CloseUTCTime", value.CloseUTCTime);
				command.Parameters.AddWithValue("@Volume", value.Volume);
				command.Parameters.AddWithValue("@QuoteAssetVolume", value.QuoteAssetVolume);
				command.Parameters.AddWithValue("@NumberOfTrades", value.NumberOfTrades);
				command.Parameters.AddWithValue("@RSI", value.RSI);
				command.Parameters.AddWithValue("@ATR", value.ATR);
				command.Parameters.AddWithValue("@EMA", value.EMA);
				command.Parameters.AddWithValue("@SMA", value.SMA);
				command.Parameters.AddWithValue("@RMA", value.RMA);

				command.Parameters.Add("@ID", SqlDbType.BigInt).Direction = ParameterDirection.Output;

				command.Connection.Open();
				try
				{
					GeneralTable.ExecuteCommand(command);
					return long.Parse(command.Parameters["@ID"].Value.ToString());
				}
				finally
				{
					command.Connection.Close();
				}
			}

			#endregion Insert Methods

			#region Update Methods

			public static void Update(Kline value)
			{
				SqlCommand command = new SqlCommand("SP_UpdateKline", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@ID", value.ID);
				command.Parameters.AddWithValue("@SymbolID", value.SymbolID);
				command.Parameters.AddWithValue("@Interval", value.Interval);
				command.Parameters.AddWithValue("@OpenUTCTime", value.OpenUTCTime);
				command.Parameters.AddWithValue("@OpenPrice", value.OpenPrice);
				command.Parameters.AddWithValue("@ClosePrice", value.ClosePrice);
				command.Parameters.AddWithValue("@HighPrice", value.HighPrice);
				command.Parameters.AddWithValue("@LowPrice", value.LowPrice);
				command.Parameters.AddWithValue("@CloseUTCTime", value.CloseUTCTime);
				command.Parameters.AddWithValue("@Volume", value.Volume);
				command.Parameters.AddWithValue("@QuoteAssetVolume", value.QuoteAssetVolume);
				command.Parameters.AddWithValue("@NumberOfTrades", value.NumberOfTrades);
				command.Parameters.AddWithValue("@RSI", value.RSI == double.NaN ? 0f : value.RSI);
				command.Parameters.AddWithValue("@ATR", value.ATR == double.NaN ? 0f : value.ATR);
				command.Parameters.AddWithValue("@EMA", value.EMA == double.NaN ? 0f : value.EMA);
				command.Parameters.AddWithValue("@SMA", value.SMA == double.NaN ? 0f : value.SMA);
				command.Parameters.AddWithValue("@RMA", value.RMA == double.NaN ? 0f : value.RMA);

				command.Connection.Open();
				try
				{
					GeneralTable.ExecuteCommand(command);
				}
				finally
				{
					command.Connection.Close();
				}
			}

			#endregion Insert Methods

			#region Delete Methods

			public static void Delete(Kline value)
			{
				SqlCommand command = new SqlCommand("SP_DeleteKline", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@ID", value.ID);

				command.Connection.Open();
				try
				{
					GeneralTable.ExecuteCommand(command);
				}
				finally
				{
					command.Connection.Close();
				}
			}

			public static void Delete(long id)
			{
				SqlCommand command = new SqlCommand("SP_DeleteKline", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@ID", id);

				command.Connection.Open();
				try
				{
					GeneralTable.ExecuteCommand(command);
				}
				finally
				{
					command.Connection.Close();
				}
			}

			public static void DeleteDynamic(string whereCondition)
			{
				SqlCommand command = new SqlCommand("SP_DeleteKlineDynamic", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@WhereCondition", whereCondition);

				command.Connection.Open();
				try
				{
					GeneralTable.ExecuteCommand(command);
				}
				finally
				{
					command.Connection.Close();
				}
			}

			#region Delete By Foreign Keys

			public static void DeleteBySymbolID(long id)
			{
				SqlCommand command = new SqlCommand("SP_DeleteKlineBySymbolID", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", id);

				command.Connection.Open();
				try
				{
					GeneralTable.ExecuteCommand(command);
				}
				finally
				{
					command.Connection.Close();
				}
			}


			public static void DeleteDuplicateCandles(long id, string interval, DateTime openTime)
			{
				SqlCommand command = new SqlCommand("SP_DeleteKlineByOpenDate", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", id);
				command.Parameters.AddWithValue("@Interval", interval);
				command.Parameters.AddWithValue("@OpenTime", openTime);
				command.Connection.Open();
				try
				{
					GeneralTable.ExecuteCommand(command);
				}
				finally
				{
					command.Connection.Close();
				}
			}

			#endregion Delete By Foreign Keys

			#endregion Delete Methods

			#region Helper Methods

			private static Kline GetObjectFromDataReader(SqlDataReader reader)
			{
				Kline value = new Kline();
				if (reader != null && !reader.IsClosed)
				{
					if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
					if (!reader.IsDBNull(reader.GetOrdinal("SymbolID"))) value.SymbolID = reader.GetInt64(reader.GetOrdinal("SymbolID"));
					if (!reader.IsDBNull(reader.GetOrdinal("Interval"))) value.Interval = reader.GetString(reader.GetOrdinal("Interval"));
					if (!reader.IsDBNull(reader.GetOrdinal("OpenUTCTime"))) value.OpenUTCTime = reader.GetDateTime(reader.GetOrdinal("OpenUTCTime"));
					if (!reader.IsDBNull(reader.GetOrdinal("OpenPrice"))) value.OpenPrice = reader.GetFloat(reader.GetOrdinal("OpenPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("ClosePrice"))) value.ClosePrice = reader.GetFloat(reader.GetOrdinal("ClosePrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("HighPrice"))) value.HighPrice = reader.GetFloat(reader.GetOrdinal("HighPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("LowPrice"))) value.LowPrice = reader.GetFloat(reader.GetOrdinal("LowPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("CloseUTCTime"))) value.CloseUTCTime = reader.GetDateTime(reader.GetOrdinal("CloseUTCTime"));
					if (!reader.IsDBNull(reader.GetOrdinal("Volume"))) value.Volume = reader.GetFloat(reader.GetOrdinal("Volume"));
					if (!reader.IsDBNull(reader.GetOrdinal("QuoteAssetVolume"))) value.QuoteAssetVolume = reader.GetFloat(reader.GetOrdinal("QuoteAssetVolume"));
					if (!reader.IsDBNull(reader.GetOrdinal("NumberOfTrades"))) value.NumberOfTrades = reader.GetInt64(reader.GetOrdinal("NumberOfTrades"));
					if (!reader.IsDBNull(reader.GetOrdinal("RSI"))) value.RSI = reader.GetFloat(reader.GetOrdinal("RSI"));
					if (!reader.IsDBNull(reader.GetOrdinal("ATR"))) value.ATR = reader.GetFloat(reader.GetOrdinal("ATR"));
					if (!reader.IsDBNull(reader.GetOrdinal("EMA"))) value.EMA = reader.GetFloat(reader.GetOrdinal("EMA"));
					if (!reader.IsDBNull(reader.GetOrdinal("SMA"))) value.SMA = reader.GetFloat(reader.GetOrdinal("SMA"));
					if (!reader.IsDBNull(reader.GetOrdinal("RMA"))) value.RMA = reader.GetFloat(reader.GetOrdinal("RMA"));

					return value;
				}

				return null;
			}

			#endregion Helper Methods

			#region Select Methods

			public static Kline Get(long id)
			{
				SqlCommand command = new SqlCommand("SP_SelectKline", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@ID", id);

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					Kline value = null;

					if (reader.Read())
						value = GetObjectFromDataReader(reader);

					if (!reader.IsClosed)
						reader.Close();

					return value;
				}
				finally
				{
					command.Connection.Close();
				}
			}
			public static List<Kline> GetLast(long symbolID,string interval)
			{
				SqlCommand command = new SqlCommand("SP_SelectLastKline", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", symbolID);
				command.Parameters.AddWithValue("@Interval", interval);
				List<Kline> values = new List<Kline>();
				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Kline value = GetObjectFromDataReader(reader);
						values.Add(value);
					}

					if (!reader.IsClosed)
						reader.Close();

					return values;
				}
				finally
				{
					command.Connection.Close();
				}
			}

			public static List<Kline> List()
			{
				SqlCommand command = new SqlCommand("SP_SelectKlineAll", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				List<Kline> values = new List<Kline>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Kline value = GetObjectFromDataReader(reader);
						values.Add(value);
					}

					if (!reader.IsClosed)
						reader.Close();

					return values;
				}
				finally
				{
					command.Connection.Close();
				}
			}

			public static List<Kline> ListDynamic(string whereCondition, string orderByExpression)
			{
				SqlCommand command = new SqlCommand("SP_SelectKlineDynamic", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@WhereCondition", whereCondition);
				command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);

				List<Kline> values = new List<Kline>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Kline value = GetObjectFromDataReader(reader);
						values.Add(value);
					}

					if (!reader.IsClosed)
						reader.Close();

					return values;
				}
				finally
				{
					command.Connection.Close();
				}
			}

			public static List<Kline> ListDynamic(string whereCondition)
			{
				SqlCommand command = new SqlCommand("SP_SelectKlineDynamic", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@WhereCondition", whereCondition);

				List<Kline> values = new List<Kline>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Kline value = GetObjectFromDataReader(reader);
						values.Add(value);
					}

					if (!reader.IsClosed)
						reader.Close();

					return values;
				}
				finally
				{
					command.Connection.Close();
				}
			}

			#region List By Foreign Keys


			public static List<Kline> ListBySymbolID(long id)
			{
				SqlCommand command = new SqlCommand("SP_SelectKlineBySymbolID", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", id);

				List<Kline> values = new List<Kline>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Kline value = GetObjectFromDataReader(reader);
						values.Add(value);
					}

					if (!reader.IsClosed)
						reader.Close();

					return values;
				}
				finally
				{
					command.Connection.Close();
				}
			}

            public static List<Kline> GetInterval(long symbolID, string interval, DateTime sDate, DateTime eDate)
            {
				SqlCommand command = new SqlCommand("SP_SelectKlinesByInterval", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", symbolID);
				command.Parameters.AddWithValue("@Interval", interval);
				command.Parameters.AddWithValue("@Start", sDate);
				command.Parameters.AddWithValue("@End", eDate);
				List<Kline> values = new List<Kline>();
				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Kline value = GetObjectFromDataReader(reader);
						values.Add(value);
					}

					if (!reader.IsClosed)
						reader.Close();

					return values;
				}
				finally
				{
					command.Connection.Close();
				}
			}

            public static List<Kline> GetInterval(long symbolID, string interval, long count)
            {
				SqlCommand command = new SqlCommand("SP_SelectKlinesByCount", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", symbolID);
				command.Parameters.AddWithValue("@Interval", interval);
				command.Parameters.AddWithValue("@Count", count);
				List<Kline> values = new List<Kline>();
				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Kline value = GetObjectFromDataReader(reader);
						values.Add(value);
					}

					if (!reader.IsClosed)
						reader.Close();

					return values;
				}
				finally
				{
					command.Connection.Close();
				}
			}
            #endregion List By Foreign Keys

            #endregion Select Methods

        }

	}
	#endregion Kline Data Provider

} // end namespace
	
	