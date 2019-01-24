# Enkompass Database Backup Converter
[![](https://img.shields.io/github/license/falahati/EnkompassDBackupConverter.svg?style=flat-square)](https://github.com/falahati/EnkompassDBackupConverter/blob/master/LICENSE)
[![](https://img.shields.io/github/commit-activity/y/falahati/EnkompassDBackupConverter.svg?style=flat-square)](https://github.com/falahati/EnkompassDBackupConverter/commits/master)
[![](https://img.shields.io/github/issues/falahati/EnkompassDBackupConverter.svg?style=flat-square)](https://github.com/falahati/EnkompassDBackupConverter/issues)

This is a simple command line tool to extract the database script from an enkompass backup file.

## Where to get
[![](https://img.shields.io/github/downloads/falahati/EnkompassDBackupConverter/total.svg?style=flat-square)](https://github.com/falahati/EnkompassDBackupConverter/releases)
[![](https://img.shields.io/github/tag-date/falahati/EnkompassDBackupConverter.svg?label=version&style=flat-square)](https://github.com/falahati/EnkompassDBackupConverter/releases)

You can download the latest compiled version from the repo's [release](https://github.com/falahati/EnkompassDBackupConverter/releases) page.

## Donation
Donations assist development and are greatly appreciated; also always remember that [every coffee counts!](https://media.makeameme.org/created/one-simply-does-i9k8kx.jpg) :)

[![](https://img.shields.io/badge/fiat-PayPal-8a00a3.svg?style=flat-square)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=WR3KK2B6TYYQ4&item_name=Donation&currency_code=USD&source=url)
[![](https://img.shields.io/badge/crypto-CoinPayments-8a00a3.svg?style=flat-square)](https://www.coinpayments.net/index.php?cmd=_donate&reset=1&merchant=820707aded07845511b841f9c4c335cd&item_name=Donate&currency=USD&amountf=20.00000000&allow_amount=1&want_shipping=0&allow_extra=1)
[![](https://img.shields.io/badge/shetab-ZarinPal-8a00a3.svg?style=flat-square)](https://zarinp.al/@falahati)

**--OR--**

You can always donate your time by contributing to the project or by introducing it to others.

## How to use
1. Extract your backup file using an extraction program.
2. Navigate to "cPanel_data\cpBackup\MYSQL_BACKUP_FILES" directory.
3. Copy "CommandLine.dll" and "EnkompassDBackupConverter.exe" files to this directory.
4. Execute the "EnkompassDBackupConverter.exe" file and wait for it to reconstruct the database backup script.

## Options
You can run the "EnkompassDBackupConverter.exe" file with "--help" or "-?" 
argument to get the list of available options.

## License
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
