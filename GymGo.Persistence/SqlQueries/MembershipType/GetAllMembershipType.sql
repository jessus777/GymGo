SELECT 
    "Id", 
    "Name", 
    "Description", 
    "Price", 
    "DurationInDays", 
    "IsActive", 
    "IsDeleted", 
    "CreatedBy", 
    "CreatedDate", 
    "LastModifiedBy", 
    "LastModifiedDate"
FROM public."MembershipTypes"
WHERE "IsActive" = True AND "IsDeleted" = False;