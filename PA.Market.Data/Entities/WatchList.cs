	using System.Data;
	using System.Data.SqlClient;
	using PropertyValidation;
    using System;
using System.Collections.Generic;
using Automation.TableProvider;
	
	namespace PA.StockMarket.Data
	{
		
	#region WatchList
	/// <summary>
	/// This object represents the properties and methods of a WatchList.
	/// </summary>

	public partial class WatchList : System.ComponentModel.INotifyPropertyChanged, ICloneable
	{
		protected long _iD;
		protected long _accountID;
		protected long _symbol;
		protected int _flag;
		
		public WatchList()
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

		public long Symbol
		{
			get {return _symbol;}
			set 
			{
				_symbol = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Symbol")); 
			}
		}

		public int Flag
		{
			get {return _flag;}
			set 
			{
				_flag = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Flag")); 
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
		
		public WatchList TypedClone()
		{
			return (WatchList)((ICloneable)this).Clone();
		}
		
		public void CopyData(WatchList value)
		{
			this.ID = value.ID;
			this.AccountID = value.AccountID;
			this.Symbol = value.Symbol;
			this.Flag = value.Flag;
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
			WatchList obj = new WatchList();
			
			obj.ID = this.ID;
			obj.AccountID = this.AccountID;
			obj.Symbol = this.Symbol;
			obj.Flag = this.Flag;
			
			return obj;
        }

        #endregion
	}
	#endregion

	//***********************************************************************************************
	
	#region WatchList Data Provider
	namespace DataAccess
	{

	internal static class WatchListDataProvider
	{
		#region Insert Methods
		
		public static long Insert(WatchList value)
		{
			SqlCommand command = new SqlCommand("SP_InsertWatchList",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@AccountID", value.AccountID);
			command.Parameters.AddWithValue("@Symbol", value.Symbol);
			command.Parameters.AddWithValue("@Flag", value.Flag);
			
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
		
		public static void Update(WatchList value)
		{
			SqlCommand command = new SqlCommand("SP_UpdateWatchList",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", value.ID);
			command.Parameters.AddWithValue("@AccountID", value.AccountID);
			command.Parameters.AddWithValue("@Symbol", value.Symbol);
			command.Parameters.AddWithValue("@Flag", value.Flag);
			
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
		
		public static void Delete(WatchList value)
		{
			SqlCommand command = new SqlCommand("SP_DeleteWatchList",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteWatchList",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteWatchListDynamic",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteWatchListByAccountID",Database.Connection);
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
		
		public static void DeleteBySymbol(long id)
		{
			SqlCommand command = new SqlCommand("SP_DeleteWatchListBySymbol",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@Symbol", id);
			
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
		
		private static WatchList GetObjectFromDataReader(SqlDataReader reader)
		{
			WatchList value = new WatchList();
			
			if (reader != null && !reader.IsClosed)
			{
				if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
				if (!reader.IsDBNull(reader.GetOrdinal("AccountID"))) value.AccountID = reader.GetInt64(reader.GetOrdinal("AccountID"));
				if (!reader.IsDBNull(reader.GetOrdinal("Symbol"))) value.Symbol = reader.GetInt64(reader.GetOrdinal("Symbol"));
				if (!reader.IsDBNull(reader.GetOrdinal("Flag"))) value.Flag = reader.GetInt32(reader.GetOrdinal("Flag"));
				
				return value;
			}
			
			return null;
		}
		
		#endregion Helper Methods
		
		#region Select Methods

		public static WatchList Get(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectWatchList",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", id);
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
			
				WatchList value = null;
			
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

		public static List<WatchList> List()
		{
			SqlCommand command = new SqlCommand("SP_SelectWatchListAll",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;

			List<WatchList> values = new List<WatchList>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
				
				while(reader.Read())
				{
					WatchList value = GetObjectFromDataReader(reader);
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

		public static List<WatchList> ListDynamic(string whereCondition,string orderByExpression)
		{
			SqlCommand command = new SqlCommand("SP_SelectWatchListDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);
			
			List<WatchList> values = new List<WatchList>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					WatchList value = GetObjectFromDataReader(reader);
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
		
		public static List<WatchList> ListDynamic(string whereCondition)
		{
			SqlCommand command = new SqlCommand("SP_SelectWatchListDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			
			List<WatchList> values = new List<WatchList>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					WatchList value = GetObjectFromDataReader(reader);
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
		
		
		public static List<WatchList> ListByAccountID(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectWatchListByAccountID",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@AccountID", id);
			
			List<WatchList> values = new List<WatchList>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					WatchList value = GetObjectFromDataReader(reader);
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

		
		public static List<WatchList> ListBySymbol(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectWatchListBySymbol",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@Symbol", id);
			
			List<WatchList> values = new List<WatchList>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					WatchList value = GetObjectFromDataReader(reader);
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
	#endregion WatchList Data Provider
	
	} // end namespace
	
	