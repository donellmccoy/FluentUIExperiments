INSERT INTO [dbo].[TypeOfWorks] ([TASK_TYPE_CODE], [TASK_TYPE_DESCRIPTION], [SELECTABLE])
VALUES
( 'C&S', 'Critical and Suspect', 1 ),
( 'C&S-NoErr', 'C&S line-item with no error', 0 ),
( 'CERT', 'Workflow - Certification', 1 ),
( 'CERT CLNUP', 'Workflow - Certification Cleanup', 1 ),
( 'DATA SUPPT', 'Workflow - Data Support', 1 ),
( 'DUPEMCN', 'Duplicate Missing Clerk Number', 0 ),
( 'Junk', 'Autocompleted JUN Document', 0 ),
( 'MAPG CLNUP', 'Workflow - Mapping Cleanup', 1 ),
( 'MAPPING', 'Workflow - Mapping', 1 ),
( 'MCN', 'Missing Clerk Number', 1 ),
( 'NPTR', 'No Pointer', 1 ),
( 'REPLAT', 'Replat', 1 ),
( 'REPRO', 'Mark for Reprocessing', 0 ),
( 'RERUNMAP', 'Rerun Mapping', 0 )
