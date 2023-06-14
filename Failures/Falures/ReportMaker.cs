using System;
using System.Collections.Generic;
using System.Linq;

namespace Failures
{
    public class ReportMaker
    {
        public enum FailureType
        {
            UnexpectedShutdown,
            ShortNonResponding,
            HardwareFailure,
            ConnectionProblem
        }

        public class Device
        {
            public string Name { get; set; }
            public int DeviceID { get; set; }
        }

        public class Failure
        {
            public int DeviceID { get; set; }
            public DateTime Time { get; set; }
            public FailureType Type { get; set; }

            public bool IsSerious()
            {
                return Type == FailureType.UnexpectedShutdown || Type == FailureType.HardwareFailure;
            }
        }

        public static List<string> FindDevicesFailedBeforeDate(DateTime date, IEnumerable<Failure> failures, IEnumerable<Device> devices)
        {
            var problematicDevices = failures
                .Where(failure => failure.IsSerious() && failure.Time < date)
                .Select(failure => failure.DeviceID)
                .ToHashSet();

            return devices
                .Where(device => problematicDevices.Contains(device.DeviceID))
                .Select(device => device.Name)
                .ToList();
        }

        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceIds,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            var date = new DateTime(year, month, day);
            var failures = new List<Failure>();

            for (var i = 0; i < failureTypes.Length; i++)
            {
                var failure = new Failure();
                failure.DeviceID = deviceIds[i];
                failure.Time = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]);
                failure.Type = (FailureType)failureTypes[i];
                failures.Add(failure);
            }

            var devicesList = devices.Select(device => new Device
            {
                Name = device["Name"] as string,
                DeviceID = Convert.ToInt32(device["DeviceId"])
            }).ToList();

            return FindDevicesFailedBeforeDate(date, failures, devicesList);
        }
    }
}
