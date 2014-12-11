<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page3.aspx.cs" Inherits="GAPIs_Calendar_v3.Page3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf8" />
    <title></title>
    <style>
        table { border-spacing: 0; border: 2px solid black; font-size: 10pt; }
        table thead tr th { border-right: 1px solid black; height: 30px; line-height: 30px; background: #ddd; }
        table tbody td { border-right: 1px solid black; border-top: 1px solid black; }
        table tbody td:last-child, table thead th:last-child { border-right: none; }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="ErrorPan" style="background: yellow; color: red" />
    <div>
        <h1 runat="server" id="AccountNameLabel" />
        <h2 runat="server" id="CalendarDescriptionLabel" />
        <a href="javascript:history.back();">Back</a>
        <asp:Repeater runat="server" ID="EventsTableRepeater">
            <HeaderTemplate>
                <div id="table-container">
                    <table id="EventsTable">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>AnyoneCanAddSelf</th>
                                <th>Attendees</th>
                                <th>AttendeesOmitted</th>
                                <th>ColorId</th>
                                <th>Created</th>
                                <th>CreatedRaw</th>
                                <th>Creator</th>
                                <th>Description</th>
                                <th>End</th>
                                <th>EndTimeUnspecified</th>
                                <th>ETag</th>
                                <th>ExtendedProperties</th>
                                <th>Gadget</th>
                                <th>GuestsCanInviteOthers</th>
                                <th>GuestsCanModify</th>
                                <th>GuestsCanSeeOtherGuests</th>
                                <th>HangoutLink</th>
                                <th>HtmlLink</th>
                                <th>ICalUID</th>
                                <th>Id</th>
                                <th>Kind</th>
                                <th>Location</th>
                                <th>Locked</th>
                                <th>Organizer</th>
                                <th>OriginalStartTime</th>
                                <th>PrivateCopy</th>
                                <th>Recurrence</th>
                                <th>RecurringEventId</th>
                                <th>Reminders</th>
                                <th>Sequence</th>
                                <th>Source</th>
                                <th>Start</th>
                                <th>Status</th>
                                <th>Summary</th>
                                <th>Transparency</th>
                                <th>Updated</th>
                                <th>UpdatedRaw</th>
                                <th>Visibility</th>
                            </tr>
                        </thead>
                        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# IncIndex() %></td>
                    <td><%# Eval("AnyoneCanAddSelf") %></td>
                    <td><%# GetAttendees(Eval("Attendees")) %></td>
                    <td><%# Eval("AttendeesOmitted") %></td>
                    <td><%# Eval("ColorId") %></td>
                    <td><%# Eval("Created") %></td>
                    <td><%# Eval("CreatedRaw") %></td>
                    <td><%# GetCreator(Eval("Creator"))%></td>
                    <td><%# Eval("Description") %></td>
                    <td><%# Eval("End.DateTime") %></td>
                    <td><%# Eval("EndTimeUnspecified") %></td>
                    <td><%# Eval("ETag") %></td>
                    <td><%# GetExtendedProperties(Eval("ExtendedProperties")) %></td>
                    <td><%# GadgetData(Eval("Gadget"))%></td>
                    <td><%# Eval("GuestsCanInviteOthers") %></td>
                    <td><%# Eval("GuestsCanModify") %></td>
                    <td><%# Eval("GuestsCanSeeOtherGuests") %></td>
                    <td><%# Eval("HangoutLink") %></td>
                    <td><%# Eval("HtmlLink") %></td>
                    <td><%# Eval("ICalUID") %></td>
                    <td><%# Eval("Id") %></td>
                    <td><%# Eval("Kind") %></td>
                    <td><%# Eval("Location") %></td>
                    <td><%# Eval("Locked") %></td>
                    <td><%# GetOrganizerData(Eval("Organizer")) %></td>
                    <td><%# Eval("OriginalStartTime") %></td>
                    <td><%# Eval("PrivateCopy") %></td>
                    <td><%# GetRecurrence(Eval("Recurrence"))%></td>
                    <td><%# Eval("RecurringEventId") %></td>
                    <td><%# GetRemindersData(Eval("Reminders")) %></td>
                    <td><%# Eval("Sequence") %></td>
                    <td><%# Eval("Source") %></td>
                    <td><%# Eval("Start.DateTime") %></td>
                    <td><%# Eval("Status") %></td>
                    <td><%# Eval("Summary") %></td>
                    <td><%# Eval("Transparency") %></td>
                    <td><%# Eval("Updated") %></td>
                    <td><%# Eval("UpdatedRaw") %></td>
                    <td><%# Eval("Visibility") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                        </tbody>
                    </table>
                    <div id="bottom_anchor"></div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
    <script>
        $("#EventsTable").find("tr").click(function () {
            $("#EventsTable").find("tr").css("background", "white");
            $(this).css("background", "yellow");
        });

        function moveScroll(){
            var scroll = $(window).scrollTop();
            var anchor_top = $("#EventsTable").offset().top;
            var anchor_bottom = $("#bottom_anchor").offset().top;
            if (scroll > anchor_top && scroll < anchor_bottom) {
                clone_table = $("#clone");
                if (clone_table.length == 0) {
                    clone_table = $("#EventsTable").clone();
                    clone_table.attr('id', 'clone');
                    clone_table.css({ position: 'absolute', 'pointer-events': 'none', top: scroll });
                    clone_table.width($("#EventsTable").width());
                    $("#table-container").append(clone_table);
                    $("#clone").css({ visibility: 'hidden' });
                    $("#clone thead").css({ visibility: "visible", "pointer-events": "auto" });
                }
                else {
                    clone_table.css("top", scroll);
                }
            } else {
                $("#clone").remove();
            }
        }
        $(window).scroll(moveScroll); 
    </script>
</body>
</html>
