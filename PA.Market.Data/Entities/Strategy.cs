	using System.Data;
	using System.Data.SqlClient;
	using PropertyValidation;
    using System;
using System.Collections.Generic;
using Automation.TableProvider;
	
	namespace PA.StockMarket.Data
	{
		
	#region Strategy
	/// <summary>
	/// This object represents the properties and methods of a Strategy.
	/// </summary>

	public partial class Strategy : System.ComponentModel.INotifyPropertyChanged, ICloneable
	{
		protected long _iD;
		protected string _strategyName = String.Empty;
		protected long _accountID;
		protected int _interval;
		protected bool _useCurrentCandle;
		
		public Strategy()
		{
		}

		#region Public Properties
		
		public long ID
		{
			get {return _iD;}
			set 
			{
				_iD = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ID")); 
			}
		}

		public string StrategyName
		{
			get {return _strategyName;}
			set 
			{
				_strategyName = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("StrategyName")); 
			}
		}

		public long AccountID
		{
			get {return _accountID;}
			set 
			{
				_accountID = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("AccountID")); 
			}
		}

		public int Interval
		{
			get {return _interval;}
			set 
			{
				_interval = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Interval")); 
			}
		}

		public bool UseCurrentCandle
		{
			get {return _useCurrentCandle;}
			set 
			{
				_useCurrentCandle = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("UseCurrentCandle")); 
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
		
		public Strategy TypedClone()
		{
			return (Strategy)((ICloneable)this).Clone();
		}
		
		public void CopyData(Strategy value)
		{
			this.ID = value.ID;
			this.StrategyName = value.StrategyName;
			this.AccountID = value.AccountID;
			this.Interval = value.Interval;
			this.UseCurrentCandle = value.UseCurrentCandle;
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
			Strategy obj = new Strategy();
			
			obj.ID = this.ID;
			obj.StrategyName = this.StrategyName;
			obj.AccountID = this.AccountID;
			obj.Interval = this.Interval;
			obj.UseCurrentCandle = this.UseCurrentCandle;
			
			return obj;
        }

        #endregion
	}
	#endregion

	//***********************************************************************************************
	
	#region Strategy Data Provider
	namespace DataAccess
	{

	public static class StrategyDataProvider
	{
		#region Insert Methods
		
		public static long Insert(Strategy value)
		{
			SqlCommand command = new SqlCommand("SP_InsertStrategy",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@StrategyName", value.StrategyName);
			command.Parameters.AddWithValue("@AccountID", value.AccountID);
			command.Parameters.AddWithValue("@Interval", value.Interval);
			command.Parameters.AddWithValue("@UseCurrentCandle", value.UseCurrentCandle);
			
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
		
		public static void Update(Strategy value)
		{
			SqlCommand command = new SqlCommand("SP_UpdateStrategy",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", value.ID);
			command.Parameters.AddWithValue("@StrategyName", value.StrategyName);
			command.Parameters.AddWithValue("@AccountID", value.AccountID);
			command.Parameters.AddWithValue("@Interval", value.Interval);
			command.Parameters.AddWithValue("@UseCurrentCandle", value.UseCurrentCandle);
			
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
		
		public static void Delete(Strategy value)
		{
			SqlCommand command = new SqlCommand("SP_DeleteStrategy",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteStrategy",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteStrategyDynamic",Database.Connection);
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
		
		public static void DeleteByAccountID(long id)
		{
			SqlCommand command = new SqlCommand("SP_DeleteStrategyByAccountID",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@AccountID", id);
			
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
		
		private static Strategy GetObjectFromDataReader(SqlDataReader reader)
		{
			Strategy value = new Strategy();
			
			if (reader != null && !reader.IsClosed)
			{
				if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
				if (!reader.IsDBNull(reader.GetOrdinal("StrategyName"))) value.StrategyName = reader.GetString(reader.GetOrdinal("StrategyName"));
				if (!reader.IsDBNull(reader.GetOrdinal("AccountID"))) value.AccountID = reader.GetInt64(reader.GetOrdinal("AccountID"));
				if (!reader.IsDBNull(reader.GetOrdinal("Interval"))) value.Interval = reader.GetInt32(reader.GetOrdinal("Interval"));
				if (!reader.IsDBNull(reader.GetOrdinal("UseCurrentCandle"))) value.UseCurrentCandle = reader.GetBoolean(reader.GetOrdinal("UseCurrentCandle"));
				
				return value;
			}
			
			return null;
		}
		
		#endregion Helper Methods
		
		#region Select Methods

		public static Strategy Get(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectStrategy",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", id);
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
			
				Strategy value = null;
			
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

		public static List<Strategy> List()
		{
			SqlCommand command = new SqlCommand("SP_SelectStrategyAll",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;

			List<Strategy> values = new List<Strategy>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
				
				while(reader.Read())
				{
					Strategy value = GetObjectFromDataReader(reader);
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

		public static List<Strategy> ListDynamic(string whereCondition,string orderByExpression)
		{
			SqlCommand command = new SqlCommand("SP_SelectStrategyDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);
			
			List<Strategy> values = new List<Strategy>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Strategy value = GetObjectFromDataReader(reader);
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
		
		public static List<Strategy> ListDynamic(string whereCondition)
		{
			SqlCommand command = new SqlCommand("SP_SelectStrategyDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			
			List<Strategy> values = new List<Strategy>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Strategy value = GetObjectFromDataReader(reader);
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
		
		
		public static List<Strategy> ListByAccountID(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectStrategyByAccountID",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@AccountID", id);
			
			List<Strategy> values = new List<Strategy>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Strategy value = GetObjectFromDataReader(reader);
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
	#endregion Strategy Data Provider
	
	} // end namespace
	
	