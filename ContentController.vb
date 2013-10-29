Imports DotNetNuke.Common.Utilities

Namespace dnnWerk.Libraries.Nuntio.Localization

    Public Class ContentController

        Public _ModuleId As Integer
        'Public _PortalId As Integer
        Public _Locale As String
        Public _ContentList As List(Of ContentInfo)

        Public Sub New(ByVal ModuleId As Integer, ByVal Locale As String, ByVal IncludeUnapproved As Boolean)
            _ModuleId = ModuleId
            '_PortalId = Portalid
            _Locale = Locale
            If IncludeUnapproved Then
                _ContentList = GetAllContent()
            Else
                _ContentList = GetActiveContent()
            End If
        End Sub

        Public Sub UpdateContent(ByVal Key As String, ByVal Content As String, ByVal PortalId As Integer, ByVal ApprovedBy As Integer, ByVal ApprovedDate As DateTime, ByVal CreatedBy As Integer, ByVal CreatedDate As DateTime, ByVal IsApproved As Boolean, ByVal IsOriginal As Boolean, ByVal SourceItemId As Integer)

            Dim objContent As New ContentInfo
            objContent.ApprovedBy = ApprovedBy
            objContent.ApprovedDate = ApprovedDate
            objContent.Content = Content
            objContent.CreatedBy = CreatedBy
            objContent.CreatedDate = CreatedDate
            objContent.IsApproved = IsApproved
            objContent.IsOriginal = IsOriginal
            objContent.Key = Key
            objContent.Locale = _Locale
            objContent.ModuleId = _ModuleId
            objContent.PortalId = PortalId
            objContent.SourceItemId = SourceItemId

            Dim nextVersion As Integer = 1

            Dim objPrevious As ContentInfo = _ContentList.Find(Function(x) (x.Key.ToUpper = Key.ToUpper AndAlso x.SourceItemId = SourceItemId AndAlso x.Locale.ToUpper = _Locale.ToUpper AndAlso x.IsApproved = True))
            If Not objPrevious Is Nothing Then
                nextVersion = objPrevious.Version + 1
            End If

            Dim previousList As List(Of ContentInfo) = GetAllContent().FindAll(Function(x) (x.Key.ToUpper = Key.ToUpper AndAlso x.SourceItemId = SourceItemId AndAlso x.Locale.ToUpper = _Locale.ToUpper))
            If Not previousList Is Nothing Then


                For Each objPreviousContent As ContentInfo In previousList

                    DotNetNuke.Data.DataProvider.Instance.ExecuteNonQuery("pnc_Localization_UpdateLocalizedItem", _
                                                                                                    objPreviousContent.ItemId, _
                                                                                                    objPreviousContent.ModuleId, _
                                                                                                    objPreviousContent.PortalId, _
                                                                                                    objPreviousContent.SourceItemId, _
                                                                                                    objPreviousContent.Locale, _
                                                                                                    objPreviousContent.Key, _
                                                                                                    objPreviousContent.Content, _
                                                                                                    False, _
                                                                                                    objPreviousContent.IsOriginal, _
                                                                                                    objPreviousContent.CreatedBy, _
                                                                                                    objPreviousContent.CreatedDate, _
                                                                                                    objPreviousContent.ApprovedBy, _
                                                                                                    objPreviousContent.ApprovedDate, _
                                                                                                    objPreviousContent.Version)
                Next
            End If

            DotNetNuke.Data.DataProvider.Instance.ExecuteNonQuery("pnc_Localization_AddLocalizedItem", _
                                                                            objContent.ModuleId, _
                                                                            objContent.PortalId, _
                                                                            objContent.SourceItemId, _
                                                                            objContent.Locale, _
                                                                            objContent.Key, _
                                                                            objContent.Content, _
                                                                            objContent.IsApproved, _
                                                                            objContent.IsOriginal, _
                                                                            objContent.CreatedBy, _
                                                                            objContent.CreatedDate, _
                                                                            objContent.ApprovedBy, _
                                                                            objContent.ApprovedDate, _
                                                                            nextVersion)

            DataCache.RemoveCache("Nuntio_LocalizedArticles_" & _ModuleId.ToString)
            DataCache.RemoveCache("Nuntio_LocalizedActiveArticles_" & _ModuleId.ToString)

            _ContentList = GetActiveContent()


        End Sub

        Public Sub DeleteContent(ByVal SourceItemId As Integer)

            DotNetNuke.Data.DataProvider.Instance.ExecuteReader("pnc_Localization_DeleteLocalizedItemsBySource", SourceItemId, _ModuleId)

        End Sub

        Public Function GetContentByKey(ByVal SourceItemId As Integer, ByVal Key As String, ByRef IsOriginal As Boolean) As String

            Dim objContent As ContentInfo = _ContentList.Find(Function(x) (x.Key.ToLower = Key.ToLower AndAlso x.SourceItemId = SourceItemId AndAlso x.Locale.ToUpper = _Locale.ToUpper AndAlso x.IsApproved = True))

            If Not objContent Is Nothing Then
                IsOriginal = objContent.IsOriginal
                Return objContent.Content
            End If

            Return ""

        End Function

        Public Function GetActiveContent() As List(Of ContentInfo)

            Dim lst As List(Of ContentInfo) = New List(Of ContentInfo)

            If Not DataCache.GetCache("Nuntio_LocalizedActiveArticles_" & _ModuleId.ToString) Is Nothing Then
                lst = CType(DataCache.GetCache("Nuntio_LocalizedActiveArticles_" & _ModuleId.ToString), List(Of ContentInfo))
                Return lst
            End If

            lst = GetAllContent.FindAll(Function(x) (x.IsApproved = True))

            DataCache.SetCache("Nuntio_LocalizedActiveArticles_" & _ModuleId.ToString, lst)

            Return lst

        End Function

        Public Function GetAllContent() As List(Of ContentInfo)

            Dim lst As List(Of ContentInfo) = New List(Of ContentInfo)

            If Not DataCache.GetCache("Nuntio_LocalizedArticles_" & _ModuleId.ToString) Is Nothing Then
                lst = CType(DataCache.GetCache("Nuntio_LocalizedArticles_" & _ModuleId.ToString), List(Of ContentInfo))
                Return lst
            End If

            lst = CBO.FillCollection(Of ContentInfo)(DotNetNuke.Data.DataProvider.Instance.ExecuteReader("pnc_Localization_GetLocalizedItems", _ModuleId))
            lst.Sort(Function(x, y) (x.CreatedDate > y.CreatedDate))

            DataCache.SetCache("Nuntio_LocalizedArticles_" & _ModuleId.ToString, lst)

            Return lst

        End Function

        Public Function GetVersions() As List(Of ContentInfo)

            Dim lst As List(Of ContentInfo) = New List(Of ContentInfo)
            lst = CBO.FillCollection(Of ContentInfo)(DotNetNuke.Data.DataProvider.Instance.ExecuteReader("pnc_Localization_GetLocalizedItems", _ModuleId)).FindAll(Function(y) (y.Key = "TITLE"))

            lst.Sort(Function(x, y) (x.Version > y.Version))

            Dim versions As New List(Of ContentInfo)
            Dim lastversion As Integer = 0

            For Each objContent As ContentInfo In lst
                If objContent.Version >= 1 Then
                    If objContent.Version <> lastversion Then
                        versions.Add(objContent)                        
                    End If
                Else
                    Exit For
                End If
                lastversion = objContent.Version
            Next

            Return versions

        End Function

    End Class

End Namespace
