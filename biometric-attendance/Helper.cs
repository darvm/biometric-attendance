﻿using Dapper;
using System;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BiometricAttendance
{
    public class Helper
    {
        public static void NumericKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        public static ModelEmployee[] GetEmployeeList(string connectionString)
        {
            try
            {
                using IDbConnection con = new SQLiteConnection(connectionString);
                var output = con.Query<ModelEmployee>("select * from employees", new DynamicParameters());
                con.Close();
                con.Dispose();
                return output.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Helper.GetEmployeeList error: {0}", ex.Message);
            }
            
            return Array.Empty<ModelEmployee>();
        }

        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        public static bool EnrollEmployee(string connectionString, int databaseId, int biometricId, string employeeId)
        {

            try
            {
                using IDbConnection con = new SQLiteConnection(connectionString);
                var output = con.Query<ModelEmployee>($"update employees set biometric_id = @biometric_id where id = @id", new { biometric_id = biometricId, id = databaseId });
                con.Close();
                con.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Helper.EnrollEmployee error: {0}", ex.Message);
            }

            return false;

        }

    }
}
