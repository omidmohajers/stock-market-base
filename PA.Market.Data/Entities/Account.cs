	using System.Data;
	using System.Data.SqlClient;
	using PropertyValidation;
    using System;
using System.Collections.Generic;
using Automation.TableProvider;
	
	namespace PA.StockMarket.Data
	{
		
	#region Account
	/// <summary>
	/// This object represents the properties and methods of a Account.
	/// </summary>

	public partial class Account : System.ComponentModel.INotifyPropertyChanged, ICloneable
	{
		protected long _iD;
		protected string _username = String.Empty;
		protected string _password = String.Empty;
		protected string _mobile = String.Empty;
		protected string _email = String.Empty;
		protected string _address = String.Empty;
		protected string _nC = String.Empty;
		protected string _lastIP = String.Empty;
		protected DateTime _lastUpdate;
		protected int _defaultTimeFrame;
		
		public Account()
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

		public string Username
		{
			get {return _username;}
			set 
			{
				_username = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Username")); 
			}
		}

		public string Password
		{
			get {return _password;}
			set 
			{
				_password = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Password")); 
			}
		}

		public string Mobile
		{
			get {return _mobile;}
			set 
			{
				_mobile = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Mobile")); 
			}
		}

		public string Email
		{
			get {return _email;}
			set 
			{
				_email = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Email")); 
			}
		}

		public string Address
		{
			get {return _address;}
			set 
			{
				_address = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Address")); 
			}
		}

		public string NC
		{
			get {return _nC;}
			set 
			{
				_nC = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("NC")); 
			}
		}

		public string LastIP
		{
			get {return _lastIP;}
			set 
			{
				_lastIP = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("LastIP")); 
			}
		}

		public DateTime LastUpdate
		{
			get {return _lastUpdate;}
			set 
			{
				_lastUpdate = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("LastUpdate")); 
			}
		}

		public int DefaultTimeFrame
		{
			get {return _defaultTimeFrame;}
			set 
			{
				_defaultTimeFrame = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("DefaultTimeFrame")); 
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
		
		public Account TypedClone()
		{
			return (Account)((ICloneable)this).Clone();
		}
		
		public void CopyData(Account value)
		{
			this.ID = value.ID;
			this.Username = value.Username;
			this.Password = value.Password;
			this.Mobile = value.Mobile;
			this.Email = value.Email;
			this.Address = value.Address;
			this.NC = value.NC;
			this.LastIP = value.LastIP;
			this.LastUpdate = value.LastUpdate;
			this.DefaultTimeFrame = value.DefaultTimeFrame;
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
			Account obj = new Account();
			
			obj.ID = this.ID;
			obj.Username = this.Username;
			obj.Password = this.Password;
			obj.Mobile = this.Mobile;
			obj.Email = this.Email;
			obj.Address = this.Address;
			obj.NC = this.NC;
			obj.LastIP = this.LastIP;
			obj.LastUpdate = this.LastUpdate;
			obj.DefaultTimeFrame = this.DefaultTimeFrame;
			
			return obj;
        }

        #endregion
	}
	#endregion

	//***********************************************************************************************
	
	#region Account Data Provider
	namespace DataAccess
	{

	public static class AccountDataProvider
	{
		#region Insert Methods
		
		public static long Insert(Account value)
		{
			SqlCommand command = new SqlCommand("SP_InsertAccount",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@Username", value.Username);
			command.Parameters.AddWithValue("@Password", value.Password);
			command.Parameters.AddWithValue("@Mobile", value.Mobile);
			command.Parameters.AddWithValue("@Email", value.Email);
			command.Parameters.AddWithValue("@Address", value.Address);
			command.Parameters.AddWithValue("@NC", value.NC);
			command.Parameters.AddWithValue("@LastIP", value.LastIP);
			command.Parameters.AddWithValue("@LastUpdate", value.LastUpdate);
			command.Parameters.AddWithValue("@DefaultTimeFrame", value.DefaultTimeFrame);
			
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
		
		public static void Update(Account value)
		{
			SqlCommand command = new SqlCommand("SP_UpdateAccount",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", value.ID);
			command.Parameters.AddWithValue("@Username", value.Username);
			command.Parameters.AddWithValue("@Password", value.Password);
			command.Parameters.AddWithValue("@Mobile", value.Mobile);
			command.Parameters.AddWithValue("@Email", value.Email);
			command.Parameters.AddWithValue("@Address", value.Address);
			command.Parameters.AddWithValue("@NC", value.NC);
			command.Parameters.AddWithValue("@LastIP", value.LastIP);
			command.Parameters.AddWithValue("@LastUpdate", value.LastUpdate);
			command.Parameters.AddWithValue("@DefaultTimeFrame", value.DefaultTimeFrame);
			
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
		
		public static void Delete(Account value)
		{
			SqlCommand command = new SqlCommand("SP_DeleteAccount",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteAccount",Database.Connection);
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
			SqlCommand command = new SqlCommand("SP_DeleteAccountDynamic",Database.Connection);
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
		
		private static Account GetObjectFromDataReader(SqlDataReader reader)
		{
			Account value = new Account();
			
			if (reader != null && !reader.IsClosed)
			{
				if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
				if (!reader.IsDBNull(reader.GetOrdinal("Username"))) value.Username = reader.GetString(reader.GetOrdinal("Username"));
				if (!reader.IsDBNull(reader.GetOrdinal("Password"))) value.Password = reader.GetString(reader.GetOrdinal("Password"));
				if (!reader.IsDBNull(reader.GetOrdinal("Mobile"))) value.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));
				if (!reader.IsDBNull(reader.GetOrdinal("Email"))) value.Email = reader.GetString(reader.GetOrdinal("Email"));
				if (!reader.IsDBNull(reader.GetOrdinal("Address"))) value.Address = reader.GetString(reader.GetOrdinal("Address"));
				if (!reader.IsDBNull(reader.GetOrdinal("NC"))) value.NC = reader.GetString(reader.GetOrdinal("NC"));
				if (!reader.IsDBNull(reader.GetOrdinal("LastIP"))) value.LastIP = reader.GetString(reader.GetOrdinal("LastIP"));
				if (!reader.IsDBNull(reader.GetOrdinal("LastUpdate"))) value.LastUpdate = reader.GetDateTime(reader.GetOrdinal("LastUpdate"));
				if (!reader.IsDBNull(reader.GetOrdinal("DefaultTimeFrame"))) value.DefaultTimeFrame = reader.GetInt32(reader.GetOrdinal("DefaultTimeFrame"));
				
				return value;
			}
			
			return null;
		}
		
		#endregion Helper Methods
		
		#region Select Methods

		public static Account Get(long id)
		{
			SqlCommand command = new SqlCommand("SP_SelectAccount",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@ID", id);
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
			
				Account value = null;
			
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

		public static List<Account> List()
		{
			SqlCommand command = new SqlCommand("SP_SelectAccountAll",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;

			List<Account> values = new List<Account>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
				
				while(reader.Read())
				{
					Account value = GetObjectFromDataReader(reader);
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

		public static List<Account> ListDynamic(string whereCondition,string orderByExpression)
		{
			SqlCommand command = new SqlCommand("SP_SelectAccountDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);
			
			List<Account> values = new List<Account>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Account value = GetObjectFromDataReader(reader);
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
		
		public static List<Account> ListDynamic(string whereCondition)
		{
			SqlCommand command = new SqlCommand("SP_SelectAccountDynamic",Database.Connection);
			command.CommandType = CommandType.StoredProcedure;
			
			command.Parameters.AddWithValue("@WhereCondition", whereCondition);
			
			List<Account> values = new List<Account>();
			
			command.Connection.Open();
			try
			{
				SqlDataReader reader = command.ExecuteReader();
	
				while(reader.Read())
				{
					Account value = GetObjectFromDataReader(reader);
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
	#endregion Account Data Provider
	
	} // end namespace
	
	