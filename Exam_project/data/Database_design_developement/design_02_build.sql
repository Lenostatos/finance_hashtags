-- For date-time columns the data type "REAL" is going to be used.
-- It contains data "as Julian day numbers, the number of days since noon in Greenwich on November 24, 4714 B.C. according to the proleptic Gregorian calendar" [SQLite docs].
-- This should suffice for at least two digits for fractional seconds.

-- Foreign keys will only be enforced if "PRAGMA foreign_keys" is set to "ON". This has to be done each time a database connection is established.
-- To do so type: "PRAGMA foreign_keys = ON;" TODO: find out how to query whether foreign_keys is already turned on before doing so.

PRAGMA foreign_keys = ON;

-- In order to avoid conflicts with SQL keywords every user-defined table and column name will start with an underscore.

DROP TABLE IF EXISTS _transaction; -- TODO: find a command for dropping all the tables at once
DROP TABLE IF EXISTS _transaction_element;
DROP TABLE IF EXISTS _tag;
DROP TABLE IF EXISTS _transaction_tagging;
DROP TABLE IF EXISTS _tag_group;
DROP TABLE IF EXISTS _tag_grouping;
DROP TABLE IF EXISTS _partner;
DROP TABLE IF EXISTS _partner_group;
DROP TABLE IF EXISTS _partner_grouping;
DROP TABLE IF EXISTS _account;
DROP TABLE IF EXISTS _account_group;
DROP TABLE IF EXISTS _account_grouping;
DROP TABLE IF EXISTS _balance;

CREATE TABLE _transaction( -- The underscore is needed because "transaction" is an SQL keyword.
	_id INTEGER PRIMARY KEY, -- AUTOINCREMENT should not be used if possible because it imposes a performance overhead and "the purpose of AUTOINCREMENT is to prevent the reuse of ROWIDs from previously deleted rows" [SQLite docs]. For further information see the documentation.
	_date REAL NOT NULL,
	_amount REAL NOT NULL,
	_description TEXT DEFAULT NULL,
	_id_account INTEGER NOT NULL REFERENCES _account ON DELETE RESTRICT, -- the "REFERENCES <table-name>" is a shortcut for referencing the primay key of the table <table-name>.
	_id_partner INTEGER DEFAULT NULL REFERENCES _partner ON DELETE RESTRICT
);
CREATE INDEX transaction_account_index ON _transaction(_id_account); -- In order to speed up the checks for foreign key violations the foreign key columns should be indexed (says the SQLite documentation).
CREATE INDEX transaction_partner_index ON _transaction(_id_partner);

CREATE TABLE _transaction_element(
	_id INTEGER PRIMARY KEY,
	_amount REAL NOT NULL,
	_description TEXT DEFAULT NULL,
	_id_transaction_element INTEGER DEFAULT NULL REFERENCES _transaction_element ON DELETE RESTRICT,
	_id_transaction INTEGER NOT NULL REFERENCES _transaction ON DELETE RESTRICT
);
CREATE INDEX transaction_element_on_itself_index ON _transaction_element(_id_transaction_element);
CREATE INDEX transaction_element_transaction_index ON _transaction_element(_id_transaction);

CREATE TABLE _tag(
	_id INTEGER PRIMARY KEY,
	_name TEXT UNIQUE NOT NULL
);

CREATE TABLE _transaction_tagging(
	_id INTEGER PRIMARY KEY,
	_id_tag INTEGER NOT NULL REFERENCES _tag ON DELETE RESTRICT,
	_id_transaction INTEGER DEFAULT NULL REFERENCES _transaction ON DELETE RESTRICT,
	_id_transaction_element INTEGER DEFAULT NULL REFERENCES _transaction_element ON DELETE RESTRICT
);
CREATE INDEX transaction_tagging_tag_index ON _transaction_tagging(_id_tag);
CREATE INDEX transaction_tagging_transaction_index ON _transaction_tagging(_id_transaction);
CREATE INDEX transaction_tagging_transaction_element_index ON _transaction_tagging(_id_transaction_element);

CREATE TABLE _tag_group(
	_id INTEGER PRIMARY KEY,
	_name TEXT UNIQUE NOT NULL
);

CREATE TABLE _tag_grouping(
	_id INTEGER PRIMARY KEY,
	_id_tag INTEGER NOT NULL REFERENCES _tag ON DELETE RESTRICT,
	_id_tag_group INTEGER DEFAULT NULL REFERENCES _tag_group ON DELETE RESTRICT
);
CREATE INDEX tag_grouping_tag_index ON _tag_grouping(_id_tag);
CREATE INDEX tag_grouping_tag_group_index ON _tag_grouping(_id_tag_group);

CREATE TABLE _partner(
	_id INTEGER PRIMARY KEY,
	_name TEXT UNIQUE NOT NULL,
	_details TEXT DEFAULT NULL
);

CREATE TABLE _partner_group(
	_id INTEGER PRIMARY KEY,
	_name TEXT UNIQUE NOT NULL
);

CREATE TABLE _partner_grouping(
	_id INTEGER PRIMARY KEY,
	_id_partner INTEGER NOT NULL REFERENCES _partner ON DELETE RESTRICT,
	_id_partner_group INTEGER DEFAULT NULL REFERENCES _partner_group ON DELETE RESTRICT
);
CREATE INDEX partner_grouping_partner_index ON _partner_grouping(_id_partner);
CREATE INDEX partner_grouping_partner_group_index ON _partner_grouping(_id_partner_group);

CREATE TABLE _account(
	_id INTEGER PRIMARY KEY,
	_name TEXT UNIQUE NOT NULL
);

CREATE TABLE _account_group(
	_id INTEGER PRIMARY KEY,
	_name TEXT UNIQUE NOT NULL
);

CREATE TABLE _account_grouping(
	_id INTEGER PRIMARY KEY,
	_id_account INTEGER NOT NULL REFERENCES _account ON DELETE RESTRICT,
	_id_account_group INTEGER DEFAULT NULL REFERENCES _account_group ON DELETE RESTRICT
);
CREATE INDEX account_grouping_account_index ON _account_grouping(_id_account);
CREATE INDEX account_grouping_account_group_index ON _account_grouping(_id_account_group);

CREATE TABLE _balance(
	_id INTEGER PRIMARY KEY,
	_date REAL NOT NULL,
	_amount REAL NOT NULL,
	_id_account INTEGER NOT NULL REFERENCES _account ON DELETE RESTRICT
);
CREATE INDEX balance_account_index ON _balance(_id_account);

--.schema _transaction
--.schema _transaction_element
--.schema _tag
--.schema _transaction_tagging