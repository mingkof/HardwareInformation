# HardwareInformation
.NET Core Cross-Platform Hardware Information Gatherer
================

![GitHub release (latest by date)](https://img.shields.io/github/v/release/L3tum/HardwareInformation?style=flat-square)
![Nuget](https://img.shields.io/nuget/v/HardwareInformation?style=flat-square)

![Build](https://github.com/L3tum/HardwareInformation/workflows/.NET%20Core%20CI/badge.svg?style=flat-square)
![Simple Test](https://github.com/L3tum/HardwareInformation/workflows/.NET%20Core%20Simple%20Test/badge.svg?style=flat-square)

  - [Features](#features)
  - [Goal](#goal)
  
## Usage

Download from [Nuget](https://www.nuget.org/packages/HardwareInformation/) via your favorite Nuget client like dotnet

`dotnet add package HardwareInformation`

Starting with version 2.1.20 HardwareInformation is also available via Github Package Registry!

Get hardware information from the gatherer

`MachinInformation info = MachineInformationGatherer.GatherInformation(bool skipClockspeedTest = true)`

Result is cached internally so don't worry about calling it multiple times

## Features

| Feature| Windows | Linux | Mac |
| ----: | ---: |  ----: | ---: |
| No Kernel driver |  :white_check_mark:  | :white_check_mark: | :white_check_mark: |
| Operating System |  :white_check_mark:  | :white_check_mark: | :white_check_mark: |
| BIOS Version*** |  :white_check_mark: | :white_check_mark: | :x: |
| BIOS Vendor*** | :white_check_mark: | :white_check_mark: | :x:
| BIOS Codename*** | :white_check_mark: | :white_check_mark: | :x:
| Mainboard Version*** | :white_check_mark: | :white_check_mark: | :x:
| Mainboard Name*** | :white_check_mark: | :white_check_mark: | :x:
| Mainboard Vendor*** | :white_check_mark: | :white_check_mark: | :x:
| CPU Physical Cores | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Logical Cores | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Logical Cores per Node**** | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Architecture | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Caption | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Name | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Vendor | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Family | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Model | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Stepping | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Type | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Max Frequency*** | :white_check_mark: | :white_check_mark:* | :x:
| CPU Base Frequency*** | :white_check_mark: | :white_check_mark:* | :white_check_mark:*
| CPU Socket*** | :white_check_mark: | :x: | :x:
| CPU Cores | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Core Number of Physical Core**** | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Core NUMA Node**** | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Core Max Frequency*** | :white_check_mark: | :white_check_mark:* | :x:
| CPU Core Base Frequency*** | :white_check_mark: | :white_check_mark:* | :x:
| CPU Core Reference Max Frequency** | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Core Reference Base Frequency** | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Core Reference Bus Frequency** | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Caches | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache Type | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache Level | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cores per Cache | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Number of times present | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache Capacity | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache CapacityHRF | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache Associativity | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache LineSize | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache Sets | :white_check_mark: | :white_check_mark: | :white_check_mark:
| CPU Cache WBINVD | :white_check_mark: | :white_check_mark: | :white_check_mark:
| **Lots** of feature flags | :white_check_mark: | :white_check_mark: | :white_check_mark:
| RAM Speed*** | :white_check_mark: | :white_check_mark:* | :x:
| RAM Manufacturer*** | :white_check_mark: | :x: | :x:
| RAM Capacity*** | :white_check_mark: | :white_check_mark:* | :x:
| RAM CapacityHRF*** | :white_check_mark: | :white_check_mark:* | :x:
| RAM Locator*** | :white_check_mark: | :x: | :x:
| RAM PartNumber (Product Name)*** | :white_check_mark: | :x: | :x:
| RAM FormFactor*** | :white_check_mark: | :white_check_mark:* | :x:
| RAM Latencies | :x: | :x: | :x:
| GPUs Vendor | :white_check_mark:*** | :white_check_mark:* | :x:
| GPUs Name | :white_check_mark:*** | :white_check_mark:* | :x:
| GPUs Driver | :white_check_mark:*** | :x: | :x:
| GPUs Status | :white_check_mark:*** | :x: | :x:
| Disks Manufacturer*** | :white_check_mark: | :white_check_mark:* | :x:
| Disks Caption*** | :white_check_mark: | :white_check_mark:* | :x:
| Disks Capacity*** | :white_check_mark: | :white_check_mark:* | :x:
| Displays Manufacturer | :white_check_mark:*** | :x: | :x:
| Displays Name | :white_check_mark:*** | :x: | :x:
| Multiple processors/Dual processors | :x: | :x: | :x:

HRF = Human Readable Format. Normal capacity/size is in bytes, while this is a string encoded with the appropriate sizing.

**\* Inaccurate or false measurements may be possible. Use with caution.**

**\*\* Only available on Intel platforms.**

**\*\*\* Accesses operating system features (Windows WMI and Linux /proc, /sys or lshw etc.) and thus may not be accurate if those features are not available.**

**\*\*\*\* Only available on AMD platforms.**

## Goal

The immediate goal is somewhat feature-parity with CPU-Z/CPUID. 
While this may be impossible for some features (like RAM latencies) without a kernel driver, part of the goal is
also to bring these kind of capabilities without requiring a software installation or kernel driver.

In some parts, this library has already more features than CPU-Z/CPUID, since it's cross-platform (for most features).

TODOs are tracked in the feature table. Crosses basically mean TODO.
