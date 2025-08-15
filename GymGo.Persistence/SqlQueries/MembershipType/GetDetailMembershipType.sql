SELECT 
    "Id", 
    "Name", 
    "Description", 
    "Price", 
    "DurationInDays", 
    "IsActive", 
    "CreatedBy", 
    "CreatedDate", 
    "LastModifiedBy", 
    "LastModifiedDate"
FROM public."MembershipTypes"
WHERE "Id" = @Id AND "IsDeleted" = False;
