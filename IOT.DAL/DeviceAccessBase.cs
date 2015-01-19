using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using IOT.Domain;
using System.Configuration;
using System.Collections.Generic;

namespace IOT.DAL
{
    public class DeviceAccessBase
    {
        public static string ConnectionString { get; set; }

        public List<DeviceBase>GetAllDevices()
        {
            var result = new List<DeviceBase>();

            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(ConnectionString))
            {
                pgsqlConnection.Open();
                string selectCommand = "getAllDevices";
                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(selectCommand, pgsqlConnection))
                {
                    using (NpgsqlTransaction tran = pgsqlConnection.BeginTransaction())
                    {
                        pgsqlcommand.CommandType = CommandType.StoredProcedure;

                        NpgsqlDataReader reader = pgsqlcommand.ExecuteReader();
                        result = ReadDevicesBase(reader);
                        tran.Commit();
                    }
                }
            }
            return result;
        }

        public DeviceBase GetDeviceById(int deviceId)
        {
            DeviceBase result = new DeviceBase();
           
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(ConnectionString))
            {
                pgsqlConnection.Open();

                string selectCommand = "getDeviceBaseById";
                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(selectCommand, pgsqlConnection))
                {
                    using (NpgsqlTransaction tran = pgsqlConnection.BeginTransaction())
                    {
                        pgsqlcommand.CommandType = CommandType.StoredProcedure;
                        pgsqlcommand.Parameters.Add(new NpgsqlParameter("deviceId", NpgsqlDbType.Integer));
                        pgsqlcommand.Parameters["deviceId"].Value = deviceId;

                        NpgsqlDataReader reader = pgsqlcommand.ExecuteReader();

                        result = ReadDeviceBase(reader);

                        tran.Commit();
                    }
                }
            }
            return result;
        }

        #region private functions
        private DeviceBase ReadDeviceBase(NpgsqlDataReader reader)
        {
            DeviceBase result = new DeviceBase();
            while (reader.Read())
            {
                result = ReadDeviceObject(reader);
            }
            return result;
        }

        private List<DeviceBase> ReadDevicesBase(NpgsqlDataReader reader)
        {
            var result = new List<DeviceBase>();
            while (reader.Read())
            {
                result.Add(ReadDeviceObject(reader));
            }
            return result;
        }

        private DeviceBase ReadDeviceObject(NpgsqlDataReader reader)
        {
            if (reader == null) { throw new ArgumentNullException("connectionName"); }

            DeviceBase device = new DeviceBase();

            device.Id = int.Parse(reader["id"].ToString());
            device.DeviceTypeId = int.Parse(reader["device_type_id"].ToString());
            device.DeviceId = int.Parse(reader["device_id"].ToString());
            device.MessageTypeId = int.Parse(reader["message_type_id"].ToString());
            device.Message = reader["message"].ToString();
            device.EventDate = DateTime.Parse(reader["event_date"].ToString());

            return device;
        }
        #endregion
    }
}
