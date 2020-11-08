using Microsoft.Ajax.Utilities;
using Mvc20201107.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace Mvc20201107.Services
{
    public class GuestbooksDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["ASP.NET.MVC"].ConnectionString;

        private readonly SqlConnection conn = new SqlConnection(cnstr);

        public List<Guesbooks> GetDataList()
        {
            List<Guesbooks> DataList = new List<Guesbooks>();
            string sql = @"Select * FROM Guestbooks";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Guesbooks Data = new Guesbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }



        //add message
        public void InSertGuestbooks(Guesbooks newData)
        {
            //sqlcommand
            //add time now
            string sql = $@"INSERT INTO Guestbooks(Name,Content,CreateTime) values('{newData.Name}','{newData.Content}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');";

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public Guesbooks GetDataById(int Id)
        {
            Guesbooks Data = new Guesbooks();

            string sql = $@"Select * from Guestbooks Where Id = {Id}";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Id = Convert.ToInt32(dr["Id"]);
                Data.Name = dr["Name"].ToString();
                Data.Content = dr["Content"].ToString();
                Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                if (!string.IsNullOrWhiteSpace(dr["Reply"].ToString()))
                {
                    Data.Reply = dr["Reply"].ToString();
                    Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                }

            }
            catch (Exception)
            {
                Data = null;
            }
            finally
            {
                conn.Close();
            }
            return Data;


        }

        public void UpdateGuestboooks(Guesbooks UpdateData)
        {
            string sql = $@"UPDATE Guestbooks Set Name='{UpdateData.Name}',Content='{UpdateData.Content}' Where Id ={UpdateData.Id}";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {

                conn.Close();
            }
        }

        public void ReplyGuestbooks(Guesbooks ReplyData)
        {

            string sql = $@"Update Guestbooks SET Reply = '{ReplyData.Reply}', ReplyTime ='{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}' Where Id = {ReplyData.Id} ";


            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public bool CheckUpdate(int Id)
        {
            Guesbooks Data = GetDataById(Id);
            return (Data != null && Data.ReplyTime == null);

        }

        public void  DeleteGuestbooks(int id)
        {
            string sql = $@"delete From Guestbooks Where id ={id};";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }




        }
        public List<Guesbooks> GetDataList(string Search)
        {
            List<Guesbooks> DataList = new List<Guesbooks>();
            string sql = string.Empty;
            if (!string.IsNullOrWhiteSpace(Search))
            {
                sql = $@"SELECT * FROM Guestbooks WHERE NAME LIKE '%{Search}%' OR Content LIKE '%{Search}%' OR Reply LIKE '%{Search}%' ; ";

            }
            else
            {

                sql = $@"SELECT * FROM Guestbooks";
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Guesbooks Data = new Guesbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }

        public List<Guesbooks> GetDataList(ForPaging Paging, string Search)
        {
            List<Guesbooks> DataList = new List<Guesbooks>();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                SetMaxPaging(Paging, Search);
                DataList = GetAllDataList(Paging, Search);
            }
            else
            {
                SetMaxPaging(Paging);
                DataList = GetAllDataList(Paging);
            }
            return DataList;
        }

        public void SetMaxPaging(ForPaging Paging)
        {
            int Row = 0;

            string sql = $@"Select  *  From Guestbooks";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    Row++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Row)/Paging.ItemNum));
            Paging.SetReightPage();
        }
        public void SetMaxPaging(ForPaging Paging, string Search)
        {
            int Row = 0;
            string sql = $@"select * From Guestbooks Where Name Like '%{Search}%' OR Content Like '%{Search}%' OR Reply Like '%{Search}%'";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Row++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString()) ;
            }
            finally
            {
                conn.Close();
            }
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Row)/Paging.ItemNum));
            Paging.SetReightPage();
        }
        public List<Guesbooks> GetAllDataList(ForPaging paging)
        {
            List<Guesbooks> DataList = new List<Guesbooks>();

            string sql = $@"select * from (select row_number() OVER(order By Id)As sort,* From Guestbooks) m Where m.sort BETWEEN {(paging.NowPage -1)*paging.ItemNum +1} AND {paging.NowPage * paging.ItemNum};";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Guesbooks Data = new Guesbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }

        public List<Guesbooks> GetAllDataList(ForPaging paging, string Search)
        {
            List<Guesbooks> DataList = new List<Guesbooks>();
            string sql = $@"Select * FROM (Select row_number() Over(order by Id)
            AS sort,* FROM Guestbooks Where Name Like '%{Search}%' OR Content LIKE
            '%{Search}%' OR Reply LIKE '%{Search}%') m WHERE m.sort Between
            {(paging.NowPage -1)*paging.ItemNum +1} AND {paging.NowPage * paging.ItemNum};";


            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Guesbooks Data = new Guesbooks();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;


        }


    }
 }
