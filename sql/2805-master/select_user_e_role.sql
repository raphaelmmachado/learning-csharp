USE [Blog]
SELECT [User].*,
       [Role].*
FROM    
       [User]
       LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
       LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]   
