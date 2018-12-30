-- For date-time columns the data type "REAL" is going to be used.
-- It contains data "as Julian day numbers, the number of days since noon in Greenwich on November 24, 4714 B.C. according to the proleptic Gregorian calendar" [SQLite docs].
-- This should suffice for at least two digits for fractional seconds.

-- Foreign keys will only be enforced if "PRAGMA foreign_keys" is set to "ON". This has to be done each time a database connection is established.
-- To do so type: "PRAGMA foreign_keys = ON;" TODO: find out how to query whether foreign_keys is already turned on before doing so.

PRAGMA foreign_keys = ON;

DROP TABLE IF EXISTS transaction_; -- TODO: find a command for dropping all the tables at once
DROP TABLE IF EXISTS transaction_element;
DROP TABLE IF EXISTS tag;
DROP TABLE IF EXISTS transaction_tagging;
DROP TABLE IF EXISTS tag_group;
DROP TABLE IF EXISTS tag_grouping;
DROP TABLE IF EXISTS partner;
DROP TABLE IF EXISTS partner_group;
DROP TABLE IF EXISTS partner_grouping;
DROP TABLE IF EXISTS account;
DROP TABLE IF EXISTS account_group;
DROP TABLE IF EXISTS account_grouping;
DROP TABLE IF EXISTS balance;

CREATE TABLE transaction_( -- The underscore is needed because "transaction" is an SQL keyword.
	id INTEGER PRIMARY KEY, -- AUTOINCREMENT should not be used if possible because it imposes a performance overhead and "the purpose of AUTOINCREMENT is to prevent the reuse of ROWIDs from previously deleted rows" [SQLite docs]. For further information see the documentation.
	date REAL UNIQUE NOT NULL,
	amount REAL NOT NULL,
	description TEXT DEFAULT NULL,
	id_account INTEGER NOT NULL REFERENCES account ON DELETE RESTRICT, -- the "REFERENCES <table-name>" is a shortcut for referencing the primay key of the table <table-name>.
	id_partner INTEGER DEFAULT NULL REFERENCES partner ON DELETE RESTRICT
);
CREATE INDEX transaction_account_index ON transaction_(id_account); -- In order to speed up the checks for foreign key violations the foreign key columns should be indexed according to the SQLite documentation.
CREATE INDEX transaction_partner_index ON transaction_(id_partner);

CREATE TABLE transaction_element(
	id INTEGER PRIMARY KEY,
	amount REAL NOT NULL,
	description TEXT DEFAULT NULL,
	id_transaction_element INTEGER DEFAULT NULL REFERENCES transaction_element ON DELETE RESTRICT,
	id_transaction INTEGER NOT NULL REFERENCES transaction_ ON DELETE RESTRICT
);
CREATE INDEX transaction_element_on_itself_index ON transaction_element(id_transaction_element);
CREATE INDEX transaction_element_transaction_index ON transaction_element(id_transaction);

CREATE TABLE tag(
	id INTEGER PRIMARY KEY,
	name TEXT UNIQUE NOT NULL
);

CREATE TABLE transaction_tagging(
	id INTEGER PRIMARY KEY,
	id_tag INTEGER NOT NULL REFERENCES tag ON DELETE RESTRICT,
	id_transaction INTEGER DEFAULT NULL REFERENCES transaction_ ON DELETE RESTRICT,
	id_transaction_element INTEGER DEFAULT NULL REFERENCES transaction_element ON DELETE RESTRICT
);
CREATE INDEX transaction_tagging_tag_index ON transaction_tagging(id_tag);
CREATE INDEX transaction_tagging_transaction_index ON transaction_tagging(id_transaction);
CREATE INDEX transaction_tagging_transaction_element_index ON transaction_tagging(id_transaction_element);

CREATE TABLE tag_group(
	id INTEGER PRIMARY KEY,
	name TEXT UNIQUE NOT NULL
);

CREATE TABLE tag_grouping(
	id INTEGER PRIMARY KEY,
	id_tag INTEGER NOT NULL REFERENCES tag ON DELETE RESTRICT,
	id_tag_group INTEGER DEFAULT NULL REFERENCES tag_group ON DELETE RESTRICT
);
CREATE INDEX tag_grouping_tag_index ON tag_grouping(id_tag);
CREATE INDEX tag_grouping_tag_group_index ON tag_grouping(id_tag_group);

CREATE TABLE partner(
	id INTEGER PRIMARY KEY,
	name TEXT UNIQUE NOT NULL,
	details TEXT DEFAULT NULL
);

CREATE TABLE partner_group(
	id INTEGER PRIMARY KEY,
	name TEXT UNIQUE NOT NULL
);

CREATE TABLE partner_grouping(
	id INTEGER PRIMARY KEY,
	id_partner INTEGER NOT NULL REFERENCES partner ON DELETE RESTRICT,
	id_partner_group INTEGER DEFAULT NULL REFERENCES partner_group ON DELETE RESTRICT
);
CREATE INDEX partner_grouping_partner_index ON partner_grouping(id_partner);
CREATE INDEX partner_grouping_partner_group_index ON partner_grouping(id_partner_group);

CREATE TABLE account(
	id INTEGER PRIMARY KEY,
	name TEXT UNIQUE NOT NULL
);

CREATE TABLE account_group(
	id INTEGER PRIMARY KEY,
	name TEXT UNIQUE NOT NULL
);

CREATE TABLE account_grouping(
	id INTEGER PRIMARY KEY,
	id_account INTEGER NOT NULL REFERENCES account ON DELETE RESTRICT,
	id_account_group INTEGER DEFAULT NULL REFERENCES account_group ON DELETE RESTRICT
);
CREATE INDEX account_grouping_account_index ON account_grouping(id_account);
CREATE INDEX account_grouping_account_group_index ON account_grouping(id_account_group);

CREATE TABLE balance(
	id INTEGER PRIMARY KEY,
	date REAL NOT NULL,
	amount REAL NOT NULL,
	id_account INTEGER NOT NULL REFERENCES account ON DELETE RESTRICT
);
CREATE INDEX balance_account_index ON balance(id_account);

--.schema transaction_
--.schema transaction_element
--.schema tag
--.schema transaction_tagging