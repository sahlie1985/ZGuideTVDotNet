' 風覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧藍
' |                                                                                                            |
' |    ZGuideTV.NET: An Electronic Program Guide (EPG) - i.e. an "electronic TV magazine"                      |
' |    - which makes the viewing of today's and next week's TV listings possible. It can be customized to      |
' |    pick up the TV listings you only want to have a look at. The application also enables you to carry out  |
' |    a search or even plan to record something later through the K!TV software.                              |
' |                                                                                                            |
' |    Copyright (C) 2004-2014 ZGuideTV.NET Team <http://zguidetv.codeplex.com/>                               |
' |                                                                                                            |
' |    Project administrator : Pascal Hubert (neojudgment@hotmail.com)                                         |
' |                                                                                                            |
' |    This program is free software: you can redistribute it and/or modify                                    |
' |    it under the terms of the GNU General Public License as published by                                    |
' |    the Free Software Foundation, either version 2 of the License, or                                       |
' |    (at your option) any later version.                                                                     |
' |                                                                                                            |
' |    This program is distributed in the hope that it will be useful,                                         |
' |    but WITHOUT ANY WARRANTY; without even the implied warranty of                                          |
' |    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                                           |
' |    GNU General Public License for more details.                                                            |
' |                                                                                                            |
' |    You should have received a copy of the GNU General Public License                                       |
' |    along with this program.  If not, see <http://www.gnu.org/licenses/>.                                   |
' |                                                                                                            |
' 風覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧藍

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
Public Class calendrier
    ' ReSharper restore InconsistentNaming
    ' ReSharper restore CheckNamespace

#Region "Property"
    ' nbJour => nombre de jours o� il y a des donn馥s dans le mois
    'Private _nbJour As Integer
    Private _nbJour As Integer
    'Public Property nbJour As Integer
    '    Get
    '        Return _nbJour
    '    End Get
    '    Set(ByVal value As Integer)
    '        _nbJour = value
    '    End Set
    ''End Property

    ' tabJour => tableau de dates sui ont des donn馥s
    'Private _tabJour As Date()
    Private _tabJour As Date()
    'Public Property tabJour As Date()
    '    Get
    '        Return _tabJour
    '    End Get
    '    Set(ByVal value As Date())
    '        _tabJour = value
    '    End Set
    'End Property

    ' Num駻o du premier jour du mois (ex Mardi:2 .... Dimanche:7)
    Private _numPremierJour As Integer

    Public Property NumPremierJour As Integer
        Get
            Return _numPremierJour
        End Get
        Set(ByVal value As Integer)
            _numPremierJour = value
        End Set
    End Property

    ' Num駻o du mois
    Private _numMois As Integer

    Public Property NumMois As Integer
        Get
            Return _numMois
        End Get
        Set(ByVal value As Integer)
            _numMois = value
        End Set
    End Property

    'Date Affich馥 dans le Mainform |
    Private _dateActif As Date

    Public Property DateActif As Date
        Get
            Return _dateActif
        End Get
        Set(ByVal value As Date)
            _dateActif = value
        End Set
    End Property

    ' Ann馥 affich馥
    Private _an As Integer

    Public Property An As Integer
        Get
            Return _an
        End Get
        Set(ByVal value As Integer)
            _an = value
        End Set
    End Property
#End Region

    Public Sub New()
        'nbJour = 0
        ReDim _tabJour(1)
        dateActif = Date.Today
    End Sub

    Public Sub Ajoute(ByVal dDate As Date)

        _nbJour += 1
        ReDim Preserve _tabJour(_nbJour - 1)
        _tabJour(_nbJour - 1) = dDate
    End Sub

    Public Function Pr駸ent(ByVal numjour As Integer, ByVal month As Integer, ByVal annee As Integer) As Boolean

        Dim i As Integer
        'Dim numjour As Integer = (nLigne * 7) - numJprem
        Dim dDate As New Date(annee, month, numjour)
        For i = 0 To (_nbJour - 1)
            If _tabJour(i).Month = dDate.Month AndAlso _tabJour(i).Day = dDate.Day AndAlso _tabJour(i).Year = dDate.Year _
                Then
                Return True
            End If
        Next i
        Return False
    End Function

    Public Sub ChangeJour(ByVal numBouton As Integer)

        Trace.WriteLine(" entree dans changejour")
        ' eviter les clics miltiples sur des changemmnts d heure, de date , ou de jour
        ' bloque les boutons le temps du traitement'230110
        Mainform.BloquerBoutonCalendrier()
        '230110 
        Mainform.navigtemporelle.EnabledButton = False

        Dim numPremierJourTmp As Integer = NumPremierJour
        If NumPremierJour = 0 Then
            numPremierJourTmp += 7
        End If
        Dim dateChoisie As New Date(An, NumMois, (numBouton - numPremierJourTmp + 1))
        dateChoisie = dateChoisie.AddHours(MomentSouhaite.Hour)


        Dim firstDate As Date = FirstDateWithData
        firstDate = firstDate.AddHours(-firstDate.Hour)
        firstDate = firstDate.AddMinutes(-firstDate.Minute)
        firstDate = firstDate.AddMilliseconds(-firstDate.Millisecond)

        ' on vient de changer la date dans monthcalendar
        Trace.WriteLine("entree month Calendar")

        If Pr駸ent(dateChoisie.Day, NumMois, An) Then
           
            Dim jourPick� As Integer
            Dim jourAffich� As Integer
            Dim difference As Integer

            DateActif = dateChoisie

            ' N駮 le 06/04/2014
            Dim differencespan As TimeSpan = (DateActif - DateTime.Now)
            Dim differenceheure As Integer = CType(differencespan.TotalHours, Integer)
            Mainform.IncrementationSourisHorizontale = differenceheure

            If dateChoisie = firstDate Then
                If dateChoisie = Date.Today() Then
                    dateChoisie = Date.Now()
                Else
                    dateChoisie = FirstDateWithData
                End If
            End If

            ' 風覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
            ' | ne pas prendre en compte du commentaire ci-dessous                |
            ' | La date choisie est endhors de ce qu'il est possible d'afficher   |
            ' | on regarde si il y a des 駑issions � d'autres heures du m麥e jour |
            ' 風覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
            If dateChoisie < FirstDateWithData OrElse dateChoisie > LastDateWithData Then

                Trace.WriteLine(" sortie de changejour")
                Mainform.AutoriserBoutonCalendrier()
                '230110
                Mainform.navigtemporelle.EnabledButton = True
                Return
            End If

            If dateChoisie > _
               DateReference.AddHours(NbDePeriodesDe_6H * NbHeuresLigneTemps) Then
                Trace.WriteLine(" rechargement des data apres ")
                Mainform.BloquerBoutonCalendrier()
                '230110 
                Mainform.navigtemporelle.EnabledButton = False
                Mainform.Timer_minute.Enabled = False

                MomentSouhaite = dateChoisie
                Mainform.ReloadData()
                Mainform.Timer_minute.Enabled = True
                Mainform.IniCalendrier(dateChoisie)
                DateReference = dateChoisie

                Trace.WriteLine(" sortie de changejour")
                Mainform.AutoriserBoutonCalendrier()
                '230110
                Mainform.navigtemporelle.ReinitBouton()
                Mainform.navigtemporelle.EnabledButton = True
                Return
            End If

            If dateChoisie < DateReference Then
                Mainform.BloquerBoutonCalendrier()
                '230110 
                Mainform.navigtemporelle.EnabledButton = False
                'load_data_before = True
                Mainform.Timer_minute.Enabled = False
                MomentSouhaite = dateChoisie
                Mainform.ReloadData()
                Mainform.Timer_minute.Enabled = True
                Mainform.IniCalendrier(dateChoisie)

                Trace.WriteLine(" sortie de changejour")
                Mainform.AutoriserBoutonCalendrier()
                '230110
                Mainform.navigtemporelle.ReinitBouton()
                Mainform.navigtemporelle.EnabledButton = True
                Return
            End If

            Mainform.IniCalendrier(dateChoisie)

            jourAffich� = DateOrigineEcran.DayOfYear
            jourPick� = dateChoisie.DayOfYear
            difference = +jourPick� - jourAffich�
            MomentSouhaite = MomentSouhaite.AddDays(difference)
            Mainform.PanelA.SendToBack()
            Mainform.DeplacerPanelA()

            Trace.WriteLine(" panelA.left apres chgmt de date" & Mainform.PanelA.Left.ToString)

            '
            Trace.WriteLine(" change jour entree dans curseur vertical")
            CentralScreen.CurseurVertical()
            'Mainform.Show()
            Trace.WriteLine(" changejour sortie de curseur vertical")
        Else
            'MessageBox.Show ("Aucun programme pour cette date.", "Aucun programme", MessageBoxButtons.OK, _
            '                MessageBoxIcon.Warning)
        End If

        Trace.WriteLine(" sortie de changejour")
        Mainform.AutoriserBoutonCalendrier()
        '230110
        Mainform.navigtemporelle.EnabledButton = True
    End Sub

    Public Function BJourAffich�(ByVal numBouton As Integer) As Boolean

        Dim dateChoisie As New Date(An, NumMois, (numBouton))
        Dim dateActifTmp As New Date(dateActif.Year, dateActif.Month, dateActif.Day)
        Return (dateChoisie = dateActifTmp)
    End Function
End Class
