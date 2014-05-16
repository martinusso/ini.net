Ini.Net
=======

A simple .NET library for managing INI files

## Features available

  * Write string `public bool WriteString(string section, string key, string value)`
  * Write integer `public bool WriteInteger(string section, string key, int value)`
  * Write boolean `public bool WriteBoolean(string section, string key, bool value)`
  * Read string `public string ReadString(string section, string key)`
  * Read boolean `public bool ReadBoolean(string section, string key)`
  * Read integer `public int ReadInteger(string section, string key)`
  * Section exists `public bool SectionExists(string section)`
  
  


## Roadmap
    
  - https://gist.github.com/evandroamparo/094ff8d209e6978dc66a
  - ~~function SectionExists(const Section: string): Boolean;~~
  - ~~function ReadInteger(const Section, Ident: string; Default: Longint): Longint;~~
  - ~~procedure WriteInteger(const Section, Ident: string; Value: Longint);~~
  - ~~function ReadBool(const Section, Ident: string; Default: Boolean): Boolean;~~
  - ~~procedure WriteBool(const Section, Ident: string; Value: Boolean);~~
  - function ReadDate(const Section, Name: string; Default: TDateTime): TDateTime; 
  - function ReadDateTime(const Section, Name: string; Default: TDateTime): TDateTime;
  - function ReadFloat(const Section, Name: string; Default: Double): Double; 
  - function ReadTime(const Section, Name: string; Default: TDateTime): TDateTime;
  - procedure WriteDate(const Section, Name: string; Value: TDateTime); 
  - procedure WriteDateTime(const Section, Name: string; Value: TDateTime);
  - procedure WriteFloat(const Section, Name: string; Value: Double);
  - procedure WriteTime(const Section, Name: string; Value: TDateTime);
  - procedure ReadSection(const Section: string; Strings: TStrings);
  - procedure ReadSections(Strings: TStrings); overload;
  - procedure ReadSections(const Section: string; Strings: TStrings); overload;
  - procedure ReadSectionValues(const Section: string; Strings: TStrings);
  - procedure EraseSection(const Section: string);
  - procedure DeleteKey(const Section, Ident: String); virtual;
  - function ValueExists(const Section, Ident: string): Boolean;
