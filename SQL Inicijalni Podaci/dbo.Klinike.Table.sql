USE [BazaZaKliniku]
GO
SET IDENTITY_INSERT [dbo].[Klinike] ON 

INSERT [dbo].[Klinike] ([ID], [nazivKlinike], [gradID], [Adresa]) VALUES (7, N'DCM', 1, N'Radanska BB')
INSERT [dbo].[Klinike] ([ID], [nazivKlinike], [gradID], [Adresa]) VALUES (8, N'Medicus', 1, N'Rade Končara 36')
INSERT [dbo].[Klinike] ([ID], [nazivKlinike], [gradID], [Adresa]) VALUES (10, N'Human', 1002, N'Zorana Đinđića 24')
INSERT [dbo].[Klinike] ([ID], [nazivKlinike], [gradID], [Adresa]) VALUES (11, N'Panajotović', 1002, N'Dragiše Cvetkovića 29')
SET IDENTITY_INSERT [dbo].[Klinike] OFF
GO
