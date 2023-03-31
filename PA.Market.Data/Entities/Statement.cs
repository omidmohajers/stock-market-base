using System.Data;
using System.Data.SqlClient;
using PropertyValidation;
using System;
using System.Collections.Generic;
using Automation.TableProvider;
using PA.StockMarket.Data;

namespace PA.StockMarket.DataEntities
{

	#region Statement
	/// <summary>
	/// This object represents the properties and methods of a Statement.
	/// </summary>

	public partial class Statement : System.ComponentModel.INotifyPropertyChanged, ICloneable
	{
		protected long _iD;
		protected long _setupID;
		protected decimal _levelPrice;
		protected DateTime _levelOpenTime;
		protected DateTime _signalOpenTime;
		protected DateTime _acceptOpenTime;
		protected decimal _tRexPrice;
		protected decimal _sLPrice;
		protected decimal _tPPrice;
		protected decimal _entryPrice;
		protected byte _state;

		public Statement()
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

		public long SetupID
		{
			get { return _setupID; }
			set
			{
				_setupID = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SetupID"));
			}
		}

		public decimal LevelPrice
		{
			get { return _levelPrice; }
			set
			{
				_levelPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("LevelPrice"));
			}
		}

		public DateTime LevelOpenTime
		{
			get { return _levelOpenTime; }
			set
			{
				_levelOpenTime = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("LevelOpenTime"));
			}
		}

		public DateTime SignalOpenTime
		{
			get { return _signalOpenTime; }
			set
			{
				_signalOpenTime = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SignalOpenTime"));
			}
		}

		public DateTime AcceptOpenTime
		{
			get { return _acceptOpenTime; }
			set
			{
				_acceptOpenTime = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("AcceptOpenTime"));
			}
		}

		public decimal TRexPrice
		{
			get { return _tRexPrice; }
			set
			{
				_tRexPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("TRexPrice"));
			}
		}

		public decimal SLPrice
		{
			get { return _sLPrice; }
			set
			{
				_sLPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("SLPrice"));
			}
		}

		public decimal TPPrice
		{
			get { return _tPPrice; }
			set
			{
				_tPPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("TPPrice"));
			}
		}
		public decimal EntryPrice
		{
			get { return _entryPrice; }
			set
			{
				_entryPrice = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("EntryPrice"));
			}
		}

		public byte State
		{
			get { return _state; }
			set
			{
				_state = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("State"));
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

		public Statement TypedClone()
		{
			return (Statement)((ICloneable)this).Clone();
		}

		public void CopyData(Statement value)
		{
			this.ID = value.ID;
			this.SetupID = value.SetupID;
			this.LevelPrice = value.LevelPrice;
			this.LevelOpenTime = value.LevelOpenTime;
			this.SignalOpenTime = value.SignalOpenTime;
			this.AcceptOpenTime = value.AcceptOpenTime;
			this.TRexPrice = value.TRexPrice;
			this.SLPrice = value.SLPrice;
			this.TPPrice = value.TPPrice;
			this.EntryPrice = value.EntryPrice;
			this.State = value.State;
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
			Statement obj = new Statement();

			obj.ID = this.ID;
			obj.SetupID = this.SetupID;
			obj.LevelPrice = this.LevelPrice;
			obj.LevelOpenTime = this.LevelOpenTime;
			obj.SignalOpenTime = this.SignalOpenTime;
			obj.AcceptOpenTime = this.AcceptOpenTime;
			obj.TRexPrice = this.TRexPrice;
			obj.SLPrice = this.SLPrice;
			obj.TPPrice = this.TPPrice;
			obj.EntryPrice = this.EntryPrice;
			obj.State = this.State;

			return obj;
		}

		#endregion
	}
	#endregion

	//***********************************************************************************************

	#region Statement Data Provider
	namespace DataAccess
	{

		public static class StatementDataProvider
		{
			#region Insert Methods

			public static long Insert(Statement value)
			{
				SqlCommand command = new SqlCommand("SP_InsertStatement", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SetupID", value.SetupID);
				command.Parameters.AddWithValue("@LevelPrice", value.LevelPrice);
				command.Parameters.AddWithValue("@LevelOpenTime", value.LevelOpenTime);
				command.Parameters.AddWithValue("@SignalOpenTime", value.SignalOpenTime);
				command.Parameters.AddWithValue("@AcceptOpenTime", value.AcceptOpenTime);
				command.Parameters.AddWithValue("@TRexPrice", value.TRexPrice);
				command.Parameters.AddWithValue("@SLPrice", value.SLPrice);
				command.Parameters.AddWithValue("@TPPrice", value.TPPrice);
				command.Parameters.AddWithValue("@EntryPrice", value.EntryPrice);
				command.Parameters.AddWithValue("@State", value.State);

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

			public static void Update(Statement value)
			{
				SqlCommand command = new SqlCommand("SP_UpdateStatement", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@ID", value.ID);
				command.Parameters.AddWithValue("@SetupID", value.SetupID);
				command.Parameters.AddWithValue("@LevelPrice", value.LevelPrice);
				command.Parameters.AddWithValue("@LevelOpenTime", value.LevelOpenTime);
				command.Parameters.AddWithValue("@SignalOpenTime", value.SignalOpenTime);
				command.Parameters.AddWithValue("@AcceptOpenTime", value.AcceptOpenTime);
				command.Parameters.AddWithValue("@TRexPrice", value.TRexPrice);
				command.Parameters.AddWithValue("@SLPrice", value.SLPrice);
				command.Parameters.AddWithValue("@TPPrice", value.TPPrice);
				command.Parameters.AddWithValue("@EntryPrice", value.EntryPrice);
				command.Parameters.AddWithValue("@State", value.State);

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

			public static void Delete(Statement value)
			{
				SqlCommand command = new SqlCommand("SP_DeleteStatement", Database.Connection);
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
				SqlCommand command = new SqlCommand("SP_DeleteStatement", Database.Connection);
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
				SqlCommand command = new SqlCommand("SP_DeleteStatementDynamic", Database.Connection);
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

			public static void DeleteBySetupID(long id)
			{
				SqlCommand command = new SqlCommand("SP_DeleteStatementBySetupID", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SetupID", id);

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

			public static void DeleteBySymbolID(long id)
			{
				SqlCommand command = new SqlCommand("SP_DeleteStatementBySymbolID", Database.Connection);
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

			#endregion Delete By Foreign Keys

			#endregion Delete Methods

			#region Helper Methods

			private static Statement GetObjectFromDataReader(SqlDataReader reader)
			{
				Statement value = new Statement();

				if (reader != null && !reader.IsClosed)
				{
					if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
					if (!reader.IsDBNull(reader.GetOrdinal("SetupID"))) value.SetupID = reader.GetInt64(reader.GetOrdinal("SetupID"));
					if (!reader.IsDBNull(reader.GetOrdinal("LevelPrice"))) value.LevelPrice = reader.GetDecimal(reader.GetOrdinal("LevelPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("LevelOpenTime"))) value.LevelOpenTime = reader.GetDateTime(reader.GetOrdinal("LevelOpenTime"));
					if (!reader.IsDBNull(reader.GetOrdinal("SignalOpenTime"))) value.SignalOpenTime = reader.GetDateTime(reader.GetOrdinal("SignalOpenTime"));
					if (!reader.IsDBNull(reader.GetOrdinal("AcceptOpenTime"))) value.AcceptOpenTime = reader.GetDateTime(reader.GetOrdinal("AcceptOpenTime"));
					if (!reader.IsDBNull(reader.GetOrdinal("TRexPrice"))) value.TRexPrice = reader.GetDecimal(reader.GetOrdinal("TRexPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("SLPrice"))) value.SLPrice = reader.GetDecimal(reader.GetOrdinal("SLPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("TPPrice"))) value.TPPrice = reader.GetDecimal(reader.GetOrdinal("TPPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("EntryPrice"))) value.EntryPrice = reader.GetDecimal(reader.GetOrdinal("EntryPrice"));
					if (!reader.IsDBNull(reader.GetOrdinal("State"))) value.State = reader.GetByte(reader.GetOrdinal("State"));

					return value;
				}

				return null;
			}

			#endregion Helper Methods

			#region Select Methods

			public static Statement Get(long id)
			{
				SqlCommand command = new SqlCommand("SP_SelectStatement", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@ID", id);

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					Statement value = null;

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

			public static List<Statement> List()
			{
				SqlCommand command = new SqlCommand("SP_SelectStatementAll", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				List<Statement> values = new List<Statement>();

				command.Connection.Open(); ;
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Statement value = GetObjectFromDataReader(reader);
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

			public static List<Statement> ListDynamic(string whereCondition, string orderByExpression)
			{
				SqlCommand command = new SqlCommand("SP_SelectStatementDynamic", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@WhereCondition", whereCondition);
				command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);

				List<Statement> values = new List<Statement>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Statement value = GetObjectFromDataReader(reader);
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

			public static List<Statement> ListDynamic(string whereCondition)
			{
				SqlCommand command = new SqlCommand("SP_SelectStatementDynamic", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@WhereCondition", whereCondition);

				List<Statement> values = new List<Statement>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Statement value = GetObjectFromDataReader(reader);
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


			public static List<Statement> ListBySetupID(long id)
			{
				SqlCommand command = new SqlCommand("SP_SelectStatementBySetupID", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SetupID", id);

				List<Statement> values = new List<Statement>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Statement value = GetObjectFromDataReader(reader);
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


			public static List<Statement> ListBySymbolID(long id)
			{
				SqlCommand command = new SqlCommand("SP_SelectStatementBySymbolID", Database.Connection);
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@SymbolID", id);

				List<Statement> values = new List<Statement>();

				command.Connection.Open();
				try
				{
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Statement value = GetObjectFromDataReader(reader);
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
	#endregion Statement Data Provider

} // end namespace
	
	