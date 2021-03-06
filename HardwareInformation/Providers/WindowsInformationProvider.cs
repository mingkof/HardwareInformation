﻿#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using HardwareInformation.Information;

#endregion

namespace HardwareInformation.Providers
{
	internal class WindowsInformationProvider : InformationProvider
	{
		public void GatherInformation(ref MachineInformation information)
		{
			var mos = new ManagementObjectSearcher(
				"select Name,NumberOfEnabledCore,NumberOfLogicalProcessors,SocketDesignation,MaxClockSpeed from Win32_Processor");

			foreach (var managementBaseObject in mos.Get())
			{
				if (managementBaseObject == null || managementBaseObject.Properties == null ||
				    managementBaseObject.Properties.Count == 0)
				{
					continue;
				}

				foreach (var propertyData in managementBaseObject.Properties)
				{
					if (propertyData == null || propertyData.Value == null || propertyData.Name == null)
					{
						continue;
					}

					switch (propertyData.Name)
					{
						case "Name":
						{
							if (information.Cpu.Name == default || information.Cpu.Name == information.Cpu.Caption)
							{
								information.Cpu.Name = propertyData.Value.ToString().Trim();
							}

							break;
						}

						// MIND THE SSSSSSSS
						case "NumberOfEnabledCore":
						{
							var val = uint.Parse(propertyData.Value.ToString());

							// Safety check
							if (information.Cpu.PhysicalCores == default ||
							    information.Cpu.PhysicalCores == information.Cpu.LogicalCores ||
							    val != 0 && val != information.Cpu.PhysicalCores)
							{
								information.Cpu.PhysicalCores = val;
							}

							break;
						}

						case "NumberOfLogicalProcessors":
						{
							var val = uint.Parse(propertyData.Value.ToString());

							if (information.Cpu.LogicalCores == default ||
							    val != 0 && val != information.Cpu.LogicalCores)
							{
								information.Cpu.LogicalCores = val;
							}

							break;
						}

						case "SocketDesignation":
						{
							if (information.Cpu.Socket == default)
							{
								information.Cpu.Socket = propertyData.Value.ToString().Trim();
							}

							break;
						}

						case "MaxClockSpeed":
						{
							if (information.Cpu.NormalClockSpeed == default)
							{
								information.Cpu.NormalClockSpeed = uint.Parse(propertyData.Value.ToString());
							}

							break;
						}
					}
				}
			}

			// There is currently no other way to gather RAM information so we don't need to check if it's already set
			mos = new ManagementObjectSearcher(
				"select ConfiguredClockSpeed,Manufacturer,Capacity,DeviceLocator,PartNumber,FormFactor from Win32_PhysicalMemory");

			foreach (var managementBaseObject in mos.Get())
			{
				if (managementBaseObject == null || managementBaseObject.Properties == null ||
				    managementBaseObject.Properties.Count == 0)
				{
					continue;
				}

				var ram = new RAM();

				foreach (var propertyData in managementBaseObject.Properties)
				{
					if (propertyData == null || propertyData.Value == null || propertyData.Name == null)
					{
						continue;
					}

					switch (propertyData.Name)
					{
						case "ConfiguredClockSpeed":
						{
							ram.Speed = uint.Parse(propertyData.Value.ToString());

							break;
						}

						case "Manufacturer":
						{
							ram.Manufacturer = propertyData.Value.ToString();

							break;
						}

						case "Capacity":
						{
							ram.Capacity += ulong.Parse(propertyData.Value.ToString());

							break;
						}

						case "DeviceLocator":
						{
							ram.Name = propertyData.Value.ToString();

							break;
						}

						case "PartNumber":
						{
							ram.PartNumber = propertyData.Value.ToString();

							break;
						}

						case "FormFactor":
						{
							ram.FormFactor = (RAM.FormFactors) Enum.Parse(
								typeof(RAM.FormFactors), propertyData.Value.ToString());

							break;
						}
					}
				}

				ram.CapacityHRF = Util.FormatBytes(ram.Capacity);

				information.RAMSticks.Add(ram);
			}

			mos = new ManagementObjectSearcher("select Name,Manufacturer,Version from Win32_BIOS");

			foreach (var managementBaseObject in mos.Get())
			{
				if (managementBaseObject == null || managementBaseObject.Properties == null ||
				    managementBaseObject.Properties.Count == 0)
				{
					continue;
				}

				foreach (var propertyData in managementBaseObject.Properties)
				{
					if (propertyData == null || propertyData.Value == null || propertyData.Name == null)
					{
						continue;
					}

					switch (propertyData.Name)
					{
						case "Name":
						{
							information.SmBios.BIOSVersion = propertyData.Value.ToString();

							break;
						}

						case "Manufacturer":
						{
							information.SmBios.BIOSVendor = propertyData.Value.ToString();

							break;
						}

						case "Version":
						{
							information.SmBios.BIOSCodename = propertyData.Value.ToString();

							break;
						}
					}
				}
			}

			mos = new ManagementObjectSearcher("select Product,Manufacturer,Version from Win32_BaseBoard");

			foreach (var managementBaseObject in mos.Get())
			{
				if (managementBaseObject == null || managementBaseObject.Properties == null ||
				    managementBaseObject.Properties.Count == 0)
				{
					continue;
				}

				foreach (var propertyData in managementBaseObject.Properties)
				{
					if (propertyData == null || propertyData.Value == null || propertyData.Name == null)
					{
						continue;
					}

					switch (propertyData.Name)
					{
						case "Product":
						{
							information.SmBios.BoardName = propertyData.Value.ToString();

							break;
						}

						case "Manufacturer":
						{
							information.SmBios.BoardVendor = propertyData.Value.ToString();

							break;
						}

						case "Version":
						{
							information.SmBios.BoardVersion = propertyData.Value.ToString();

							break;
						}
					}
				}
			}

			mos = new ManagementObjectSearcher("select Model,Size,Caption from Win32_DiskDrive");

			foreach (var managementBaseObject in mos.Get())
			{
				var disk = new Disk();

				foreach (var propertyData in managementBaseObject.Properties)
				{
					switch (propertyData.Name)
					{
						case "Model":
						{
							disk.Model = propertyData.Value.ToString();
							break;
						}
						case "Size":
						{
							disk.Capacity = ulong.Parse(propertyData.Value.ToString());
							disk.CapacityHRF = Util.FormatBytes(disk.Capacity);
							break;
						}
						case "Caption":
						{
							disk.Caption = propertyData.Value.ToString();
							break;
						}
					}
				}

				information.Disks.Add(disk);
			}

			mos = new ManagementObjectSearcher(
				"select AdapterCompatibility,Caption,Description,DriverDate,DriverVersion,Name,Status from Win32_VideoController");

			foreach (var managementBaseObject in mos.Get())
			{
				var gpu = new GPU();

				foreach (var propertyData in managementBaseObject.Properties)
				{
					switch (propertyData.Name)
					{
						case "AdapterCompatibility":
						{
							gpu.Vendor = propertyData.Value.ToString();

							break;
						}
						case "Caption":
						{
							gpu.Caption = propertyData.Value.ToString();

							break;
						}
						case "DriverDate":
						{
							gpu.DriverDate = propertyData.Value.ToString();

							break;
						}
						case "DriverVersion":
						{
							gpu.DriverVersion = propertyData.Value.ToString();

							break;
						}
						case "Description":
						{
							gpu.Description = propertyData.Value.ToString();

							break;
						}
						case "Name":
						{
							gpu.Name = propertyData.Value.ToString();

							break;
						}
						case "Status":
						{
							gpu.Status = propertyData.Value.ToString();

							break;
						}
					}
				}

				information.Gpus.Add(gpu);
			}

			mos = new ManagementObjectSearcher("root\\wmi",
				"select ManufacturerName,UserFriendlyName from WmiMonitorID");

			foreach (var managementBaseObject in mos.Get())
			{
				try
				{
					var display = new Display();

					foreach (var propertyData in managementBaseObject.Properties)
					{
						switch (propertyData.Name)
						{
							case "ManufacturerName":
							{
								display.Manufacturer = string.Join("", ((IEnumerable<ushort>) propertyData.Value)
									.Select(u => char.ConvertFromUtf32(u)).Where(s => s != "\u0000").ToList());
								break;
							}
							case "UserFriendlyName":
							{
								display.Name = string.Join("", ((IEnumerable<ushort>) propertyData.Value)
									.Select(u => char.ConvertFromUtf32(u)).Where(s => s != "\u0000").ToList());
								break;
							}
						}
					}

					information.Displays.Add(display);
				}
				catch
				{
					// Intentionally left blank
				}
			}
		}

		public bool Available(MachineInformation information)
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
		}

		public void PostProviderUpdateInformation(ref MachineInformation information)
		{
			// Intentionally left blank
		}
	}
}