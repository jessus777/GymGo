-- Consulta de datos paginados
SELECT 
    "Id",
    "Name",
    "Description",
    "Price",
    "DurationInDays",
    "IsActive",
    "CreatedBy"
FROM  public."MembershipTypes"
WHERE 
    "IsDeleted" = False
    AND (
        @SearchValue IS NULL 
        OR "Name" LIKE CAST(@SearchValue AS TEXT) 
        OR "Description" LIKE CAST(@SearchValue AS TEXT)
    )
ORDER BY {OrderBy} {OrderDir}
OFFSET {Offset} ROWS
FETCH NEXT {Limit} ROWS ONLY;

-- Total general
SELECT COUNT(*) FROM public."MembershipTypes" WHERE "IsDeleted" = False;

-- Total filtrado
SELECT COUNT(*) 
FROM public."MembershipTypes"
WHERE 
    "IsDeleted" = False
    AND (@SearchValue IS NULL OR "Name" LIKE CAST(@SearchValue AS TEXT) OR "Description" LIKE CAST(@SearchValue AS TEXT));