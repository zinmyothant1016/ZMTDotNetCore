﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ZMTDotNetCore.ConsoleApp.Services;

namespace ZMTDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder ConnectionString;

        public AdoDotNetExample(SqlConnectionStringBuilder ConnectionString)
        {
            ConnectionString = ConnectionString;
        }


        //public AdoDotNetExample()
        // {
        //     stringBuilder = new SqlConnectionStringBuilder();
        //     ConnectionString = "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=DotNetTrainingBatch4;User ID=sa0;Password=sa@12345;";
        // }
        public void Read()
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = "select * from Tbl_Blog";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection Close");
            //dataset ==> data table
            //data table==> data row
            //data row ==> data column
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog Id =>" + dr["BlogId"]);
                Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
                Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
                Console.WriteLine("Blog Contenct =>" + dr["BlogContent"]);
                Console.WriteLine("--------------------");
            }
        }
        public void Create(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogId],
            [BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ( @Id,
            @BlogTitle
           , @BlogAuthor
           ,@BlogContent );";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            Console.WriteLine(message);
        }
        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ConnectionString);
            connection.Open();
            string query = "select * from Tbl_Blog where BlogId=@Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            Console.WriteLine("Connection Close");
            //dataset ==> data table
            //data table==> data row
            //data row ==> data column
            DataRow dr = dt.Rows[0];
            Console.WriteLine("Blog Id =>" + dr["BlogId"]);
            Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
            Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
            Console.WriteLine("Blog Contenct =>" + dr["BlogContent"]);
            Console.WriteLine("--------------------");

        }
        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ConnectionString);
            connection.Open();
            string query = "DELETE Tbl_Blog where BlogId=@Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result > 0 ? "Delete Successfully!" : "Delete Fail!");
        }
        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.ConnectionString);
            connection.Open();
            string query = @"Update Tbl_Blog set [BlogTitle]=@BlogTitle,[BlogAuthor]=@BlogAuthor,[BlogContent]=@BlogContent where BlogId=@Id;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@Id", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result > 0 ? "Update Successfully!" : "Update Fail!");
        }
    }
}
