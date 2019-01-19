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
	
	
-- Property tags() As IList(Of Tag):
-- Get
-- session.Fetch(Of Tag_Group)(query)
SELECT "Tag"."id", "Tag"."name" FROM "Tag"
	JOIN "Tag_Grouping" AS "Tag_Ging" ON "Tag"."id" = "Tag_Ging"."id_Tag"
	WHERE "Tag_Ging"."id_Tag_Group" = @p0; -- Me.id
-- Set(value As IList(Of Tag))
--session.Delete(query)
DELETE FROM "Tag_Grouping" AS "T_Ging"
WHERE "T_Ging"."id_Tag_Group" = @p0; -- 0: Me.id
-- For Each item in value
-- session.Insert(query)
INSERT INTO "Tag_Grouping" ("id_tag", "id_tag_group") VALUES (@p0, @p1); -- 0: item.id ; 1: Me.id
	
-- Member function has_tag(tag As Tag) As Boolean:
-- session.Single(Of Integer)(query)
SELECT COUNT(*) FROM "Tag_Group" AS "TG"
	JOIN "Tag_Grouping" As "TGing" ON "TG"."id" = "TGing"."id_Tag_Group"
	JOIN "Tag" As "T" ON "TGing"."id_Tag" = "T"."id"
	WHERE "TG"."id" = @p0 AND "T"."id" = @p1; -- 0: Me.id ; 1: tag.id
	
SELECT COUNT(*) FROM "Tag_Grouping" AS "T_Ging"
	WHERE "T_Ging"."id_tag_group" = @p0 AND "T_Ging"."id_tag" = @p1; -- 0: Me.id ; 1: tag.id
	
-- Member procedure add_tag(tag As Tag):
-- session.Insert(New Tag_Grouping(tag.id, Me.id)) -- Probably executing the query below directly is better.
-- session.Advanced.Execute(query)
INSERT INTO "Tag_Grouping" ("id_tag", "id_tag_group") VALUES (@p0, @p1); -- 0: tag.id ; 1: Me.id

-- Member procedure remove_tag(tag As Tag):
-- exceptions: If tag is a not a part of Me. If tag is the last tag in the group.
-- session.Advanced.Execute(query)
DELETE FROM "Tag_Grouping" AS "T_Ging"
WHERE "T_Ging"."id_Tag_Group" = @p0 AND "T_Ging"."id_Tag" = @p1; -- 0: Me.id ; 1: tag.id


DELETE FROM "Tag_Group";
DELETE FROM "Tag_Grouping";
DELETE FROM "Tag";