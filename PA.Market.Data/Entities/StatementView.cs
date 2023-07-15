using System.Data;
using System.Data.SqlClient;
using PropertyValidation;
using System;
using System.Collections.Generic;

namespace PA.StockMarket.Data
{

    #region StatementView
    /// <summary>
    /// This object represents the properties and methods of a StatementView.
    /// </summary>

    public partial class StatementView : System.ComponentModel.INotifyPropertyChanged, ICloneable
    {
        protected long _iD;
        protected string _name = String.Empty;
        protected string _description = String.Empty;
        protected long _createBy;
        protected long _symbolID;
        protected string _structureInterval = String.Empty;
        protected string _triggerInterval = String.Empty;
        protected long _statementID;
        protected decimal _levelPrice;
        protected DateTime _levelOpenTime;
        protected DateTime _signalOpenTime;
        protected DateTime _acceptOpenTime;
        protected decimal _tRexPrice;
        protected decimal _sLPrice;
        protected decimal _tPPrice;
        protected decimal _entryPrice;
        protected byte _state;
        protected long _accountID;

        public StatementView()
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

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Name"));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Description"));
            }
        }

        public long CreateBy
        {
            get { return _createBy; }
            set
            {
                _createBy = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("CreateBy"));
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

        public string StructureInterval
        {
            get { return _structureInterval; }
            set
            {
                _structureInterval = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("StructureInterval"));
            }
        }

        public string TriggerInterval
        {
            get { return _triggerInterval; }
            set
            {
                _triggerInterval = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("TriggerInterval"));
            }
        }

        public long StatementID
        {
            get { return _statementID; }
            set
            {
                _statementID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("StatementID"));
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

        public long AccountID
        {
            get { return _accountID; }
            set
            {
                _accountID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("AccountID"));
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

        public StatementView TypedClone()
        {
            return (StatementView)((ICloneable)this).Clone();
        }

        public void CopyData(StatementView value)
        {
            this.ID = value.ID;
            this.Name = value.Name;
            this.Description = value.Description;
            this.CreateBy = value.CreateBy;
            this.SymbolID = value.SymbolID;
            this.StructureInterval = value.StructureInterval;
            this.TriggerInterval = value.TriggerInterval;
            this.StatementID = value.StatementID;
            this.LevelPrice = value.LevelPrice;
            this.LevelOpenTime = value.LevelOpenTime;
            this.SignalOpenTime = value.SignalOpenTime;
            this.AcceptOpenTime = value.AcceptOpenTime;
            this.TRexPrice = value.TRexPrice;
            this.SLPrice = value.SLPrice;
            this.TPPrice = value.TPPrice;
            this.EntryPrice = value.EntryPrice;
            this.State = value.State;
            this.AccountID = value.AccountID;
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
            StatementView obj = new StatementView();

            obj.ID = this.ID;
            obj.Name = this.Name;
            obj.Description = this.Description;
            obj.CreateBy = this.CreateBy;
            obj.SymbolID = this.SymbolID;
            obj.StructureInterval = this.StructureInterval;
            obj.TriggerInterval = this.TriggerInterval;
            obj.StatementID = this.StatementID;
            obj.LevelPrice = this.LevelPrice;
            obj.LevelOpenTime = this.LevelOpenTime;
            obj.SignalOpenTime = this.SignalOpenTime;
            obj.AcceptOpenTime = this.AcceptOpenTime;
            obj.TRexPrice = this.TRexPrice;
            obj.SLPrice = this.SLPrice;
            obj.TPPrice = this.TPPrice;
            obj.EntryPrice = this.EntryPrice;
            obj.State = this.State;
            obj.AccountID = this.AccountID;

            return obj;
        }

        #endregion
    }
    #endregion

    //***********************************************************************************************

    #region StatementView Data Provider
    namespace DataAccess
    {

        public static class StatementViewDataProvider
        {


            #region Helper Methods

            private static StatementView GetObjectFromDataReader(SqlDataReader reader)
            {
                StatementView value = new StatementView();

                if (reader != null && !reader.IsClosed)
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("ID"))) value.ID = reader.GetInt64(reader.GetOrdinal("ID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("Name"))) value.Name = reader.GetString(reader.GetOrdinal("Name"));
                    if (!reader.IsDBNull(reader.GetOrdinal("Description"))) value.Description = reader.GetString(reader.GetOrdinal("Description"));
                    if (!reader.IsDBNull(reader.GetOrdinal("CreateBy"))) value.CreateBy = reader.GetInt64(reader.GetOrdinal("CreateBy"));
                    if (!reader.IsDBNull(reader.GetOrdinal("SymbolID"))) value.SymbolID = reader.GetInt64(reader.GetOrdinal("SymbolID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("StructureInterval"))) value.StructureInterval = reader.GetString(reader.GetOrdinal("StructureInterval"));
                    if (!reader.IsDBNull(reader.GetOrdinal("TriggerInterval"))) value.TriggerInterval = reader.GetString(reader.GetOrdinal("TriggerInterval"));
                    if (!reader.IsDBNull(reader.GetOrdinal("StatementID"))) value.StatementID = reader.GetInt64(reader.GetOrdinal("StatementID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("LevelPrice"))) value.LevelPrice = reader.GetDecimal(reader.GetOrdinal("LevelPrice"));
                    if (!reader.IsDBNull(reader.GetOrdinal("LevelOpenTime"))) value.LevelOpenTime = reader.GetDateTime(reader.GetOrdinal("LevelOpenTime"));
                    if (!reader.IsDBNull(reader.GetOrdinal("SignalOpenTime"))) value.SignalOpenTime = reader.GetDateTime(reader.GetOrdinal("SignalOpenTime"));
                    if (!reader.IsDBNull(reader.GetOrdinal("AcceptOpenTime"))) value.AcceptOpenTime = reader.GetDateTime(reader.GetOrdinal("AcceptOpenTime"));
                    if (!reader.IsDBNull(reader.GetOrdinal("TRexPrice"))) value.TRexPrice = reader.GetDecimal(reader.GetOrdinal("TRexPrice"));
                    if (!reader.IsDBNull(reader.GetOrdinal("SLPrice"))) value.SLPrice = reader.GetDecimal(reader.GetOrdinal("SLPrice"));
                    if (!reader.IsDBNull(reader.GetOrdinal("TPPrice"))) value.TPPrice = reader.GetDecimal(reader.GetOrdinal("TPPrice"));
                    if (!reader.IsDBNull(reader.GetOrdinal("EntryPrice"))) value.EntryPrice = reader.GetDecimal(reader.GetOrdinal("EntryPrice"));
                    if (!reader.IsDBNull(reader.GetOrdinal("State"))) value.State = reader.GetByte(reader.GetOrdinal("State"));
                    if (!reader.IsDBNull(reader.GetOrdinal("AccountID"))) value.AccountID = reader.GetInt64(reader.GetOrdinal("AccountID"));

                    return value;
                }

                return null;
            }

            #endregion Helper Methods

            #region Select Methods

            public static StatementView Get(long id)
            {
                SqlCommand command = new SqlCommand("SP_SelectStatementView", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", id);

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    StatementView value = null;

                    if (reader.Read())
                        value = GetObjectFromDataReader(reader);

                    if (!reader.IsClosed)
                        reader.Close();

                    return value;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static List<StatementView> List()
            {
                SqlCommand command = new SqlCommand("SP_SelectStatementViewAll", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                List<StatementView> values = new List<StatementView>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatementView value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static List<StatementView> ListDynamic(string whereCondition, string orderByExpression)
            {
                SqlCommand command = new SqlCommand("SP_SelectStatementViewDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);
                command.Parameters.AddWithValue("@OrderByExpression", orderByExpression);

                List<StatementView> values = new List<StatementView>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatementView value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            public static List<StatementView> ListDynamic(string whereCondition)
            {
                SqlCommand command = new SqlCommand("SP_SelectStatementViewDynamic", Database.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@WhereCondition", whereCondition);

                List<StatementView> values = new List<StatementView>();

                Database.Connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatementView value = GetObjectFromDataReader(reader);
                        values.Add(value);
                    }

                    if (!reader.IsClosed)
                        reader.Close();

                    return values;
                }
                finally
                {
                    Database.Connection.Close();
                }
            }

            #endregion Select Methods

        }

    }
    #endregion StatementView Data Provider

} // end namespace

