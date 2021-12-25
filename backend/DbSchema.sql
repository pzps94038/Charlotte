USE [master]
GO
/****** Object:  Database [Charlotte]    Script Date: 2021/12/25 下午 11:02:22 ******/
CREATE DATABASE [Charlotte]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Charlotte', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Charlotte.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Charlotte_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Charlotte_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Charlotte] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Charlotte].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Charlotte] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Charlotte] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Charlotte] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Charlotte] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Charlotte] SET ARITHABORT OFF 
GO
ALTER DATABASE [Charlotte] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Charlotte] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Charlotte] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Charlotte] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Charlotte] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Charlotte] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Charlotte] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Charlotte] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Charlotte] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Charlotte] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Charlotte] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Charlotte] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Charlotte] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Charlotte] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Charlotte] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Charlotte] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Charlotte] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Charlotte] SET RECOVERY FULL 
GO
ALTER DATABASE [Charlotte] SET  MULTI_USER 
GO
ALTER DATABASE [Charlotte] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Charlotte] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Charlotte] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Charlotte] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Charlotte] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Charlotte] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Charlotte', N'ON'
GO
ALTER DATABASE [Charlotte] SET QUERY_STORE = OFF
GO
USE [Charlotte]
GO
/****** Object:  Table [dbo].[UserMain]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMain](
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UserAccount] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[UserId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_UserMain] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokenStatus]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokenStatus](
	[RefreshToken] [nvarchar](450) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RefreshTokenStatus] PRIMARY KEY CLUSTERED 
(
	[RefreshToken] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_RefreshToken]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_RefreshToken]
AS
SELECT          dbo.RefreshTokenStatus.UserId, dbo.RefreshTokenStatus.RefreshToken, dbo.UserMain.Email, 
                            dbo.RefreshTokenStatus.CreateDate
FROM              dbo.RefreshTokenStatus INNER JOIN
                            dbo.UserMain ON dbo.RefreshTokenStatus.UserId = dbo.UserMain.UserId
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[ProductTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductDetails]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetails](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[ProductImgPath] [nvarchar](max) NULL,
	[Inventory] [int] NULL,
	[SellPrice] [int] NOT NULL,
	[CostPrice] [int] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
 CONSTRAINT [PK_ProductDetails] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ProductId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Total] [int] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_orderDetail]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_orderDetail]
AS
SELECT          dbo.UserMain.UserId, dbo.UserMain.UserName, dbo.ProductDetails.SellPrice, dbo.ProductDetails.ProductName, 
                            dbo.ProductType.Type, dbo.OrderDetail.Amount, dbo.OrderDetail.Total, dbo.OrderDetail.CreateDate
FROM              dbo.OrderDetail INNER JOIN
                            dbo.ProductDetails ON dbo.OrderDetail.ProductId = dbo.ProductDetails.ProductId INNER JOIN
                            dbo.ProductType ON dbo.ProductDetails.ProductTypeId = dbo.ProductType.ProductTypeId INNER JOIN
                            dbo.UserMain ON dbo.OrderDetail.UserId = dbo.UserMain.UserId
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2021/12/25 下午 11:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetail_UserId]    Script Date: 2021/12/25 下午 11:02:22 ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetail_UserId] ON [dbo].[OrderDetail]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductDetails_ProductTypeId]    Script Date: 2021/12/25 下午 11:02:22 ******/
CREATE NONCLUSTERED INDEX [IX_ProductDetails_ProductTypeId] ON [dbo].[ProductDetails]
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RefreshTokenStatus_UserId]    Script Date: 2021/12/25 下午 11:02:22 ******/
CREATE NONCLUSTERED INDEX [IX_RefreshTokenStatus_UserId] ON [dbo].[RefreshTokenStatus]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductDetails] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateDate]
GO
ALTER TABLE [dbo].[UserMain] ADD  DEFAULT (N'') FOR [UserAccount]
GO
ALTER TABLE [dbo].[UserMain] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_ProductDetails_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ProductDetails] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_ProductDetails_ProductId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_UserMain_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserMain] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_UserMain_UserId]
GO
ALTER TABLE [dbo].[ProductDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductDetails_ProductType_ProductTypeId] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([ProductTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductDetails] CHECK CONSTRAINT [FK_ProductDetails_ProductType_ProductTypeId]
GO
ALTER TABLE [dbo].[RefreshTokenStatus]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokenStatus_UserMain_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserMain] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RefreshTokenStatus] CHECK CONSTRAINT [FK_RefreshTokenStatus_UserMain_UserId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "OrderDetail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 239
               Right = 215
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductDetails"
            Begin Extent = 
               Top = 6
               Left = 241
               Bottom = 231
               Right = 407
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductType"
            Begin Extent = 
               Top = 6
               Left = 459
               Bottom = 261
               Right = 626
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserMain"
            Begin Extent = 
               Top = 6
               Left = 669
               Bottom = 253
               Right = 874
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_orderDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_orderDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RefreshTokenStatus"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserMain"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 136
               Right = 407
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_RefreshToken'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_RefreshToken'
GO
USE [master]
GO
ALTER DATABASE [Charlotte] SET  READ_WRITE 
GO
