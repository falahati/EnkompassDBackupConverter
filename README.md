Enkompass Database Backup Converter
================

This is a simple command line tool to extract the database script from an enkompass backup file.

How to use
=============
1. Extract your backup file using an extraction program
2. Navigate to "cPanel_data\cpBackup" directory
3. Copy "CommandLine.dll" and "EnkompassDBackupConverter.exe" files to this directory; 
right next to "MYSQL_BACKUP_FILES" subdirectory.
4. Execute the "EnkompassDBackupConverter.exe" file and wait for it to reconstruct
the database backup script.

Options
=============
You can run the "EnkompassDBackupConverter.exe" file with "--help" or "-?" 
argument to get the list of available options.

License
=============
Copyright (C) 2013 Soroush Falahati - soroush@falahati.net

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA