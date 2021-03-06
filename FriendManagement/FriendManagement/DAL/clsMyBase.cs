﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FriendManagement.DAL
{
    class clsMyBase
    {
        protected string _Error;

        public string Error 
        {   
            get 
                {
                    return _Error;
                }
        }

        protected SqlConnection cn = new SqlConnection();

        protected bool Connection()
        {
            if (cn.State == ConnectionState.Open)
            {
                return true;
            }
            cn.ConnectionString = FriendManagement.Properties.Settings.Default.Mycon;

            try
            {
                cn.Open();
                return true;
            }
            catch (Exception ex)
            {

                _Error = ex.Message;
            }
            return false;
        }

        protected SqlCommand MyCommand;
        protected SqlDataReader MyReader;

        protected SqlCommand MycommandBuilder(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = sql;
            return cmd;
        }

        protected bool ExecuteNonQuery(SqlCommand cmd)
        {
            if (!Connection())
            
                return false;
            
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                _Error = ex.Message;
            }
            return false;
        }

        protected DataSet ExecuteDataSet(string sql)
        {
            DataSet ds = new DataSet();

            if (!Connection())
                return null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = sql;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;


        }
        
    }
}
