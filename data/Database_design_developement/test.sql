.mode column
.headers on

PRAGMA foreign_keys = ON;

DELETE FROM "Tag_Group";
DELETE FROM "Tag_Grouping";
DELETE FROM "Tag";

INSERT INTO "Tag_Group" ("id", "name") VALUES (1, "Food");
INSERT INTO "Tag_Group" ("id", "name") VALUES (2, "Non-Veggie");

INSERT INTO "Tag" ("id", "name") VALUES (1, "Fruit");
INSERT INTO "Tag" ("id", "name") VALUES (2, "Vegetable");
INSERT INTO "Tag" ("id", "name") VALUES (3, "Fish");
INSERT INTO "Tag" ("id", "name") VALUES (4, "Loan");

INSERT INTO "Tag_Grouping" ("id", "id_tag", "id_tag_group") VALUES (1, 1, 1);
INSERT INTO "Tag_Grouping" ("id", "id_tag", "id_tag_group") VALUES (2, 2, 1);
INSERT INTO "Tag_Grouping" ("id", "id_tag", "id_tag_group") VALUES (3, 3, 1);
INSERT INTO "Tag_Grouping" ("id", "id_tag", "id_tag_group") VALUES (4, 3, 2);

SELECT * FROM "Tag"
	JOIN "Tag_Grouping" As "Tag_Ging" ON "Tag"."id" = "Tag_Ging"."id_tag"
	JOIN "Tag_Group" As "Tag_G" ON "Tag_Ging"."id_Tag_Group" = "Tag_G"."id";

-- 1 is a replacement for a variable.
	
-- Member function get_tags:
SELECT "Tag"."id", "Tag"."name" FROM "Tag"
	JOIN "Tag_Grouping" AS "Tag_Ging" ON "Tag"."id" = "Tag_Ging"."id_Tag"
	WHERE "Tag_Ging"."id_Tag_Group" = 1; -- Me.id
	
-- Member function has_tag(Tag):
SELECT COUNT(*) FROM "Tag_Group" AS "TG"
	JOIN "Tag_Grouping" As "TGing" ON "TG"."id" = "TGing"."id_Tag_Group"
		WHERE "TG"."id" = 1 -- Me.id
	JOIN "Tag" As "T" ON "TGing"."id_Tag" = "T"."id"
		WHERE "T"."id" = 1; -- Tag.id