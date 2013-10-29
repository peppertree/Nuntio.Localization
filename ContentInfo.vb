Namespace dnnWerk.Libraries.Nuntio.Localization

    Public Class ContentInfo
        Implements DotNetNuke.Entities.Modules.IHydratable

        Private _ItemId As Integer
        Private _content As String
        Private _key As String
        Private _ApprovedBy As Integer
        Private _ApprovedDate As DateTime
        Private _CreatedBy As Integer
        Private _CreatedDate As DateTime
        Private _IsOriginal As Boolean
        Private _IsApproved As Boolean
        Private _Locale As String
        Private _ModuleId As Integer
        Private _PortalId As Integer
        Private _SourceItemId As Integer
        Private _Version As Integer

        Public Property ItemId As Integer
            Get
                Return _ItemId
            End Get
            Set(ByVal Value As Integer)
                _ItemId = Value
            End Set
        End Property

        Public Property Content As String
            Get
                Return _content
            End Get
            Set(ByVal Value As String)
                _content = Value
            End Set
        End Property

        Public Property Key As String
            Get
                Return _key
            End Get
            Set(ByVal Value As String)
                _key = Value
            End Set
        End Property

        Public Property ApprovedBy As Integer
            Get
                Return _ApprovedBy
            End Get
            Set(ByVal Value As Integer)
                _ApprovedBy = Value
            End Set
        End Property

        Public Property ApprovedDate As DateTime
            Get
                Return _ApprovedDate
            End Get
            Set(ByVal Value As DateTime)
                _ApprovedDate = Value
            End Set
        End Property

        Public Property CreatedBy As Integer
            Get
                Return _CreatedBy
            End Get
            Set(ByVal Value As Integer)
                _CreatedBy = Value
            End Set
        End Property

        Public Property CreatedDate As DateTime
            Get
                Return _CreatedDate
            End Get
            Set(ByVal Value As DateTime)
                _CreatedDate = Value
            End Set
        End Property

        Public Property IsOriginal As Boolean
            Get
                Return _IsOriginal
            End Get
            Set(ByVal Value As Boolean)
                _IsOriginal = Value
            End Set
        End Property

        Public Property IsApproved As Boolean
            Get
                Return _IsApproved
            End Get
            Set(ByVal Value As Boolean)
                _IsApproved = Value
            End Set
        End Property

        Public Property Locale As String
            Get
                Return _Locale
            End Get
            Set(ByVal Value As String)
                _Locale = Value
            End Set
        End Property

        Public Property ModuleId As Integer
            Get
                Return _ModuleId
            End Get
            Set(ByVal Value As Integer)
                _ModuleId = Value
            End Set
        End Property

        Public Property PortalId As Integer
            Get
                Return _PortalId
            End Get
            Set(ByVal Value As Integer)
                _PortalId = Value
            End Set
        End Property

        Public Property SourceItemId As Integer
            Get
                Return _SourceItemId
            End Get
            Set(ByVal Value As Integer)
                _SourceItemId = Value
            End Set
        End Property

        Public Property Version As Integer
            Get
                Return _Version
            End Get
            Set(ByVal Value As Integer)
                _Version = Value
            End Set
        End Property

        Public Sub Fill(ByVal dr As System.Data.IDataReader) Implements DotNetNuke.Entities.Modules.IHydratable.Fill

            If Not IsDBNull(dr("ItemId")) Then
                Integer.TryParse(dr("ItemId"), _ItemId)
            End If

            If Not IsDBNull(dr("ApprovedBy")) Then
                Integer.TryParse(dr("ApprovedBy"), _ApprovedBy)
            End If

            If Not IsDBNull(dr("ApprovedDate")) Then
                Date.TryParse(dr("ApprovedDate"), _ApprovedDate)
            End If

            If Not IsDBNull(dr("CreatedBy")) Then
                Integer.TryParse(dr("CreatedBy"), _CreatedBy)
            End If

            If Not IsDBNull(dr("ModuleId")) Then
                Integer.TryParse(dr("ModuleId"), _ModuleId)
            End If

            If Not IsDBNull(dr("PortalId")) Then
                Integer.TryParse(dr("PortalId"), _PortalId)
            End If

            If Not IsDBNull(dr("SourceItemId")) Then
                Integer.TryParse(dr("SourceItemId"), _SourceItemId)
            End If

            If Not IsDBNull(dr("Version")) Then
                Integer.TryParse(dr("Version"), _Version)
            End If

            If Not IsDBNull(dr("CreatedDate")) Then
                Date.TryParse(dr("CreatedDate"), _CreatedDate)
            End If

            If Not IsDBNull(dr("Content")) Then
                _content = Convert.ToString(dr("Content"))
            End If

            If Not IsDBNull(dr("Key")) Then
                _key = Convert.ToString(dr("Key"))
            End If

            If Not IsDBNull(dr("Locale")) Then
                _Locale = Convert.ToString(dr("Locale"))
            End If

            If Not IsDBNull(dr("IsApproved")) Then
                Boolean.TryParse(dr("IsApproved"), _IsApproved)
            End If

            If Not IsDBNull(dr("IsOriginal")) Then
                Boolean.TryParse(dr("IsOriginal"), _IsOriginal)
            End If

        End Sub

        Public Property KeyID As Integer Implements DotNetNuke.Entities.Modules.IHydratable.KeyID
            Get
                Return _ItemId
            End Get
            Set(ByVal value As Integer)
                _ItemId = value
            End Set
        End Property

    End Class

End Namespace
