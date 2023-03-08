	using System.Data;
	using System.Data.SqlClient;
	using PropertyValidation;
    using System;
using System.Collections.Generic;
using Automation.TableProvider;
	
	namespace PA.StockMarket.Data
	{
		
	#region Symbol
	/// <summary>
	/// This object represents the properties and methods of a Symbol.
	/// </summary>

	public partial class Symbol : System.ComponentModel.INotifyPropertyChanged, ICloneable
	{
		protected long _iD;
		protected string _name = String.Empty;
		protected long _marketID;
		
		public Symbol()
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

		public string Name
		{
			get {return _name;}
			set 
			{
				_name = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Name")); 
			}
		}

		public long MarketID
		{
			get {return _marketID;}
			set 
			{
				_marketID = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("MarketID")); 
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
		
		public Symbol TypedClone()
		{
			return (Symbol)((ICloneable)this).Clone();
		}
		
		public void CopyData(Symbol value)
		{
			this.ID = value.ID;
			this.Name = value.Name;
			this.MarketID = value.MarketID;
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
			Symbol obj = new Symbol();
			
			obj.ID = this.ID;
			obj.Name = this.Name;
			obj.MarketID = this.MarketID;
			
			return obj;
        }

        #endregion
	}
	#endregion

	//***********************************************************************************************
	
	#region Symbol Data Provider
	namespace DataAccess
	{

	public static class SymbolDataProvider
	{
		#region Insert Methods
		
		public static long Insert(Symbol value)
		{
			SqlCommand command = new SqlCommand("SP_InsertSymbol",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@Name", value.Name);
			command.Parameters.AddWithValue("@MarketID", value.MarketID);
			
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
		
		public static void Update(Symbol value)
		{
			SqlCommand command = new SqlCommand("SP_UpdateSymbol",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", value.ID);
			command.Parameters.AddWithValue("@Name", value.Name);
			command.Parameters.AddWithValue("@MarketID", value.MarketID);
			
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
		
		public static void Delete(Symbol value)
		{
			SqlCommand command = new SqlCommand("SP_DeleteSymbol",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteSymbol",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteSymbolDynamic",Database.Connection);
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
		
		public static void DeleteByMarketID(long id)
		{
			SqlCommand command = new SqlCommand("SP_DeleteSymbolByMarketID",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@MarketID", id);
			
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
		
		private static Symbol GetObjectFromDataReader(SqlDataReader reader)
		{
			Symbol value = new Symbol();
			
			if (reader != null && !reader.IsClosed)
			{
				if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
				if (!reader.IsDBNull(reader.GetOrdinal("Name"))) value.Name = reader.GetString(reader.GetOrdinal("Name"));
				if (!reader.IsDBNull(reader.GetOrdinal("MarketID"))) value.MarketID = reader.GetInt64(reader.GetOrdinal("MarketID"));
				
				return value;
			}
			
			return null;
		}
		
		#endregion Helper Methods
		
		#region Select Methods

		public static Symbol Get(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectSymbol",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", id);
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
			
				Symbol value = null;
			
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

		public static List<Symbol> List()
		{
			SqlCommand command = new SqlCommand("SP_SelectSymbolAll",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;

			List<Symbol> values = new List<Symbol>();

				command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
				
				while(reader.Read())
				{
					Symbol value = GetObjectFromDataReader(reader);
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

		public static List<Symbol> ListDynamic(string whereCondition,string orderByExpression)
		{
			SqlCommand command = new SqlCommand("SP_SelectSymbolDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);
			
			List<Symbol> values = new List<Symbol>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Symbol value = GetObjectFromDataReader(reader);
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
		
		public static List<Symbol> ListDynamic(string whereCondition)
		{
			SqlCommand command = new SqlCommand("SP_SelectSymbolDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			
			List<Symbol> values = new List<Symbol>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Symbol value = GetObjectFromDataReader(reader);
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
		
		
		public static List<Symbol> ListByMarketID(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectSymbolByMarketID",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@MarketID", id);
			
			List<Symbol> values = new List<Symbol>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Symbol value = GetObjectFromDataReader(reader);
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
	#endregion Symbol Data Provider
	
	} // end namespace
	
	