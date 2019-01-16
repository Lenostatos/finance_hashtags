-- For date-time columns the data type "REAL" is going to be used.
-- It contains data "as Julian day numbers, the number of days since noon in Greenwich on November 24, 4714 B.C. according to the proleptic Gregorian calendar" [SQLite docs].
-- This should suffice for at least two digits for fractional seconds.

-- Foreign keys will only be enforced if "PRAGMA foreign_keys" is set to "ON". This has to be done each time a database connection is established.
-- To do so type: "PRAGMA foreign_keys = ON;" TODO: find out how to query whether foreign_keys is already turned on before doing so.

PRAGMA foreign_keys = ON;

-- In order to avoid conflicts with SQL keywords every user-defined name has to be enclosed in double quotes.

-- TODO: find a command for dropping all the tables at once
DROP TABLE IF EXISTS "Transaction_Tagging";
DROP TABLE IF EXISTS "Transaction";
DROP TABLE IF EXISTS "Transaction_Element";
DROP TABLE IF EXISTS "Tag_Grouping";
DROP TABLE IF EXISTS "Tag";
DROP TABLE IF EXISTS "Tag_Group";
DROP TABLE IF EXISTS "Partner_Grouping";
DROP TABLE IF EXISTS "Partner";
DROP TABLE IF EXISTS "Partner_Group";
DROP TABLE IF EXISTS "Account_Grouping";
DROP TABLE IF EXISTS "Account";
DROP TABLE IF EXISTS "Account_Group";
DROP TABLE IF EXISTS "Balance";

CREATE TABLE "Transaction"(
	"id" INTEGER PRIMARY KEY, -- AUTOINCREMENT should not be used if possible because it imposes a performance overhead and "the purpose of AUTOINCREMENT is to prevent the reuse of ROWIDs from previously deleted rows" [SQLite docs]. For further information see the documentation.
	"date" REAL NOT NULL,
	"amount" REAL NOT NULL,
	"description" TEXT DEFAULT NULL,
	"id_Account" INTEGER NOT NULL REFERENCES "Account" ON DELETE RESTRICT, -- the "REFERENCES <table-name>" is a shortcut for referencing the primay key of the table <table-name>.
	"id_Partner" INTEGER DEFAULT NULL REFERENCES "Partner" ON DELETE RESTRICT
);
-- In order to speed up the checks for foreign key violations the foreign key columns should be indexed (says the SQLite documentation).
CREATE INDEX "transaction_account_index" ON "Transaction"("id_Account");
CREATE INDEX "transaction_partner_index" ON "Transaction"("id_Partner");

-- Every column that might be subject to a WHERE clause should be indexed to improve performance.
-- TODO: Add missing indices.
CREATE INDEX "transaction_date_index" ON "Transaction"("date");

CREATE TABLE "Transaction_Element"(
	"id" INTEGER PRIMARY KEY,
	"amount" REAL NOT NULL,
	"description" TEXT DEFAULT NULL,
	"id_Transaction_element" INTEGER DEFAULT NULL REFERENCES "Transaction_Element" ON DELETE RESTRICT,
	"id_Transaction" INTEGER NOT NULL REFERENCES "Transaction" ON DELETE RESTRICT
);
CREATE INDEX "transaction_element_on_itself_index" ON "Transaction_Element"("id_Transaction_element");
CREATE INDEX "transaction_element_transaction_index" ON "Transaction_Element"("id_Transaction");

CREATE TABLE "Tag"(
	"id" INTEGER PRIMARY KEY,
	"name" TEXT UNIQUE NOT NULL
);

CREATE TABLE "Transaction_Tagging"(
	"id" INTEGER PRIMARY KEY,
	"id_Tag" INTEGER NOT NULL REFERENCES "Tag" ON DELETE RESTRICT,
	"id_Transaction" INTEGER DEFAULT NULL REFERENCES "Transaction" ON DELETE RESTRICT, -- TODO: Change RESTRICT to something appropriate
	"id_Transaction_element" INTEGER DEFAULT NULL REFERENCES "Transaction_Element" ON DELETE RESTRICT
);
CREATE INDEX "transaction_tagging_tag_index" ON "Transaction_Tagging"("id_Tag");
CREATE INDEX "transaction_tagging_transaction_index" ON "Transaction_Tagging"("id_Transaction");
CREATE INDEX "transaction_tagging_transaction_element_index" ON "Transaction_Tagging"("id_Transaction_element");

CREATE TABLE "Tag_Group"(
	"id" INTEGER PRIMARY KEY,
	"name" TEXT UNIQUE NOT NULL
);

CREATE TABLE "Tag_Grouping"(
	"id" INTEGER PRIMARY KEY,
	"id_Tag" INTEGER NOT NULL REFERENCES "Tag" ON DELETE CASCADE,
	"id_Tag_Group" INTEGER NOT NULL REFERENCES "Tag_Group" ON DELETE CASCADE
);
CREATE INDEX "tag_grouping_tag_index" ON "Tag_Grouping"("id_Tag");
CREATE INDEX "tag_grouping_tag_group_index" ON "Tag_Grouping"("id_Tag_Group");

CREATE TABLE "Partner"(
	"id" INTEGER PRIMARY KEY,
	"name" TEXT UNIQUE NOT NULL,
	"details" TEXT DEFAULT NULL
);

CREATE TABLE "Partner_Group"(
	"id" INTEGER PRIMARY KEY,
	"name" TEXT UNIQUE NOT NULL
);

CREATE TABLE "Partner_Grouping"(
	"id" INTEGER PRIMARY KEY,
	"id_Partner" INTEGER NOT NULL REFERENCES "Partner" ON DELETE RESTRICT,
	"id_Partner_Group" INTEGER DEFAULT NULL REFERENCES "Partner_Group" ON DELETE RESTRICT
);
CREATE INDEX "partner_grouping_partner_index" ON "Partner_Grouping"("id_Partner");
CREATE INDEX "partner_grouping_partner_group_index" ON "Partner_Grouping"("id_Partner_Group");

CREATE TABLE "Account"(
	"id" INTEGER PRIMARY KEY,
	"name" TEXT UNIQUE NOT NULL
);

CREATE TABLE "Account_Group"(
	"id" INTEGER PRIMARY KEY,
	"name" TEXT UNIQUE NOT NULL
);

CREATE TABLE "Account_Grouping"(
	"id" INTEGER PRIMARY KEY,
	"id_Account" INTEGER NOT NULL REFERENCES "Account" ON DELETE RESTRICT,
	"id_Account_Group" INTEGER DEFAULT NULL REFERENCES "Account_Group" ON DELETE RESTRICT
);
CREATE INDEX "account_grouping_account_index" ON "Account_Grouping"("id_Account");
CREATE INDEX "account_grouping_account_group_index" ON "Account_Grouping"("id_Account_Group");

CREATE TABLE "Balance"(
	"id" INTEGER PRIMARY KEY,
	"date" REAL NOT NULL,
	"amount" REAL NOT NULL,
	"id_Account" INTEGER NOT NULL REFERENCES "Account" ON DELETE RESTRICT
);
CREATE INDEX "balance_account_index" ON "Balance"("id_Account");