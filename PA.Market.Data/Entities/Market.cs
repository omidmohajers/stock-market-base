using System.Data;
using System.Data.SqlClient;
using PropertyValidation;
using System;
using System.Collections.Generic;
using Automation.TableProvider;
	
	namespace PA.StockMarket.Data
	{
		
	#region Market
	/// <summary>
	/// This object represents the properties and methods of a Market.
	/// </summary>

	public partial class Market : System.ComponentModel.INotifyPropertyChanged, ICloneable
	{
		protected long _iD;
		protected string _name = String.Empty;
		protected string _apiVer = String.Empty;
		protected string _description = String.Empty;
		
		public Market()
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

		public string ApiVer
		{
			get {return _apiVer;}
			set 
			{
				_apiVer = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ApiVer")); 
			}
		}

		public string Description
		{
			get {return _description;}
			set 
			{
				_description = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Description")); 
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
		
		public Market TypedClone()
		{
			return (Market)((ICloneable)this).Clone();
		}
		
		public void CopyData(Market value)
		{
			this.ID = value.ID;
			this.Name = value.Name;
			this.ApiVer = value.ApiVer;
			this.Description = value.Description;
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
			Market obj = new Market();
			
			obj.ID = this.ID;
			obj.Name = this.Name;
			obj.ApiVer = this.ApiVer;
			obj.Description = this.Description;
			
			return obj;
        }

        #endregion
	}
	#endregion

	//***********************************************************************************************
	
	#region Market Data Provider
	namespace DataAccess
	{

	public static class MarketDataProvider
	{
		#region Insert Methods
		
		public static long Insert(Market value)
		{
			SqlCommand command = new SqlCommand("SP_InsertMarket",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@Name", value.Name);
			command.Parameters.AddWithValue("@ApiVer", value.ApiVer);
			command.Parameters.AddWithValue("@Description", value.Description);
			
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
		
		public static void Update(Market value)
		{
			SqlCommand command = new SqlCommand("SP_UpdateMarket",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", value.ID);
			command.Parameters.AddWithValue("@Name", value.Name);
			command.Parameters.AddWithValue("@ApiVer", value.ApiVer);
			command.Parameters.AddWithValue("@Description", value.Description);
			
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
		
		public static void Delete(Market value)
		{
			SqlCommand command = new SqlCommand("SP_DeleteMarket",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteMarket",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteMarketDynamic",Database.Connection);
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
		
			#endregion Delete By Foreign Keys
			
		#endregion Delete Methods
		
		#region Helper Methods
		
		private static Market GetObjectFromDataReader(SqlDataReader reader)
		{
			Market value = new Market();
			
			if (reader != null && !reader.IsClosed)
			{
				if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
				if (!reader.IsDBNull(reader.GetOrdinal("Name"))) value.Name = reader.GetString(reader.GetOrdinal("Name"));
				if (!reader.IsDBNull(reader.GetOrdinal("ApiVer"))) value.ApiVer = reader.GetString(reader.GetOrdinal("ApiVer"));
				if (!reader.IsDBNull(reader.GetOrdinal("Description"))) value.Description = reader.GetString(reader.GetOrdinal("Description"));
				
				return value;
			}
			
			return null;
		}
		
		#endregion Helper Methods
		
		#region Select Methods

		public static Market Get(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectMarket",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", id);
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
			
				Market value = null;
			
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

		public static List<Market> List()
		{
			SqlCommand command = new SqlCommand("SP_SelectMarketAll",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;

			List<Market> values = new List<Market>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
				
				while(reader.Read())
				{
					Market value = GetObjectFromDataReader(reader);
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

		public static List<Market> ListDynamic(string whereCondition,string orderByExpression)
		{
			SqlCommand command = new SqlCommand("SP_SelectMarketDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);
			
			List<Market> values = new List<Market>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Market value = GetObjectFromDataReader(reader);
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
		
		public static List<Market> ListDynamic(string whereCondition)
		{
			SqlCommand command = new SqlCommand("SP_SelectMarketDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			
			List<Market> values = new List<Market>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Market value = GetObjectFromDataReader(reader);
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
		

			#endregion List By Foreign Keys

		#endregion Select Methods

	}
	
	}
	#endregion Market Data Provider
	
	} // end namespace
	
	