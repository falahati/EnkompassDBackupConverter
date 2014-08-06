Enkompass Database Backup Converter
================

This is a simple command line tool to extract the database script from an enkompass backup file.

How to use
=============
1. Extract your backup file using an extraction program.
2. Navigate to "cPanel_data\cpBackup\MYSQL_BACKUP_FILES" directory.
3. Copy "CommandLine.dll" and "EnkompassDBackupConverter.exe" files to this directory.
4. Execute the "EnkompassDBackupConverter.exe" file and wait for it to reconstruct
the database backup script.

Options
=============
You can run the "EnkompassDBackupConverter.exe" file with "--help" or "-?" 
argument to get the list of available options.

License
=============
Copyright (C) 2013 Soroush Falahati - soroush@falahati.net

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see [http://www.gnu.org/licenses/].