using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace pre.test.pages
{
  public class UpdateBookedRecording : BasePage
  {
    public static string date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
    public UpdateBookedRecording(IPage page) : base(page)
    {
    }
    private String stringCourt = "Birmingham";
    private String stringCase = "";

    public async Task NavigateToBooking()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
    }

    public async Task FindCaseToView()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
      .Locator("div:nth-child(19) div.virtualized-gallery div:nth-child(1) > div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(2)");
      stringCase = caseLocation.TextContentAsync().Result.ToString().Trim();
      stringCase = stringCase.Substring(stringCase.LastIndexOf(':') + 1);

      var courtLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
      .Locator("div:nth-child(19)  div.virtualized-gallery > div > div > div:nth-child(1)  div:nth-child(4)");
      stringCourt = courtLocation.TextContentAsync().Result.ToString().Trim();
      stringCourt = stringCourt.Substring(stringCourt.LastIndexOf(':') + 1);
    }

    public async Task BookCase()
    {
      if (UpdateBookedRecordings.use == "W"){
        stringCase = $"UpdateWitAutoTest{date}";
      }else if (UpdateBookedRecordings.use == "D"){
        stringCase = $"UpdateDefAutoTest{date}";
      }else{
        stringCase = $"UpdateDefWitAutoTest{date}";
      }

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase}");

      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "defendants 1,\ndefendants 2");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]",
          "Witness surname1,\nWitness surname2");
      await Page.Frame("fullscreen-app-host").ClickAsync(":nth-match(button:has-text(\"Save\"), 2)");
    }


    public async Task ScheduleUpdateRecording()
    {
      var day = DateTime.UtcNow.ToString("ddd");
      var month = DateTime.UtcNow.ToString("MMM");
      var date = DateTime.UtcNow.ToString("dd");
      var year = DateTime.UtcNow.ToString("yyyy");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ Scheduled\\ Start\\ DateOpen\\ calendar\\ to\\ select\\ a\\ date\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"{day}\\ {month}\\ {date}\\ {year}\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync("button[role='button']:has-text(\"Ok\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Witness\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Witness\\ items\"] div:has-text(\"Witness surname1\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ your\\ Defendants\"]");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Select\\ your\\ Defendants\\ items\"] div:has-text(\"defendants 1\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Save\")");
    }

    public async Task SearchCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]",$"{stringCase.Trim()}");
    }
    public async Task FindCase()
    {
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result, Does.Contain($"{stringCourt}")));

      var caseName = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseName.TextContentAsync().Result, Does.Contain($"{stringCase.Trim()}")));
    }
    public async Task UpdateCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
       await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]","UpdateWitness 1,\nUpdateWitness 2");
      var saveButton = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("div div:nth-child(55)  button");
      await Task.Run(() => Assert.IsTrue(saveButton.IsEnabledAsync().Result));

      await Task.Run(() => saveButton.ClickAsync());
    }

    public async Task CheckUpdatedCase()
    {

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase}");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      var WitnessBox = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Task.Run(() => Assert.That(WitnessBox.InputValueAsync().Result, Does.Contain("UpdateWitness 1")));
      await Task.Run(() => Assert.That(WitnessBox.InputValueAsync().Result, Does.Contain("UpdateWitness 2")));
      // await Task.Run(() => Assert.That(WitnessBox.InputValueAsync().Result, Does.Not.Contain("Witness surname1")));
      // await Task.Run(() => Assert.That(WitnessBox.InputValueAsync().Result, Does.Not.Contain("Witness surname2")));

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Manage Recordings\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Search\\ Case\\ Ref\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Search\\ Case\\ Ref\"] ", $"{stringCase}");

      var updatecaseScheduled = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator($"div.virtualized-gallery:has-text(\"UpdateAutoTest{date}\")");
      await Task.Run(() =>
        Assert.That(updatecaseScheduled.TextContentAsync().Result, Does.Contain($"{stringCase}")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Amend\")");
      await Page.Frame("fullscreen-app-host")
        .ClickAsync("[aria-label=\"Defendants\\.\\ Selected\\:\\ Witness\\ surname1\"]");

      var witnessdropdown = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("li[role='option']");

      await Task.Run(() => Assert.That(witnessdropdown.AllInnerTextsAsync().Result, Does.Contain("UpdateWitness 1")));
      await Task.Run(() => Assert.That(witnessdropdown.AllInnerTextsAsync().Result, Does.Contain("UpdateWitness 2")));
      // await Task.Run(() => Assert.That(witnessdropdown.AllInnerTextsAsync().Result, Does.Not.Contain("Witness surname1")));
      // await Task.Run(() => Assert.That(witnessdropdown.AllInnerTextsAsync().Result, Does.Not.Contain("Witness surname2")));
    }


   

    public async Task SearchupdatedefCase()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase}");
    }

    public async Task FindupdatedefCase()
    {
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result, Does.Contain($"{stringCourt}")));

      var caseName = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseName.TextContentAsync().Result, Does.Contain($"{stringCase}")));
    }


    public async Task UpdateCasedef()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "Updatedef 1,\nUpdatedef 2");
      var saveButton = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("div div:nth-child(55)  button");
      await Task.Run(() => Assert.IsTrue(saveButton.IsEnabledAsync().Result));

      await Task.Run(() => saveButton.ClickAsync());

      var test = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("[aria-label='Select Scheduled Start Date']");
      await Task.Run(() => Assert.True(test.IsEnabledAsync().Result));
      await Task.Run(() => test.ClickAsync());
    }

    public async Task CheckUpdatedCasedef()
    {
      var home = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Home\")");
      await Task.Run(() => Assert.True(home.IsEnabledAsync().Result));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");

      var book = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")");
      await Task.Run(() => Assert.True(book.IsEnabledAsync().Result));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");


      var court = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("[aria-label=\"Select\\ Court\"]");
      await Task.Run(() => Assert.True(court.IsEnabledAsync().Result));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");
      await Page.Frame("fullscreen-app-host").ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"UpdatedefAutoTest{date}");
        await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      var defBox = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Contain("Updatedef 1")));
      await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Contain("Updatedef 2")));
    }

    public async Task SearchupdatedefwitCase()
    {

      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase}");
    }

    public async Task FindupdatedefwitCase()
    {
      var caseLocation = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("div.canvasContentDiv.container_1vt1y2p > div > div:nth-child(3)");
      await Task.Run(() => Assert.That(caseLocation.TextContentAsync().Result, Does.Contain($"{stringCourt}")));

      var caseName = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "#publishedCanvas div.canvasContentDiv.container_1vt1y2p div div:nth-child(1) div div div div div");
      await Task.Run(() => Assert.That(caseName.TextContentAsync().Result, Does.Contain($"{stringCase}")));
    }




    public async Task UpdateCasedefandwit()
    {
      await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]", "Updatedef 1,\nUpdatedef 2");
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Page.Frame("fullscreen-app-host").FillAsync("[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]","UpdateWitness 1,\nUpdateWitness 2");
      var saveButton = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("div div:nth-child(55)  button");
      await Task.Run(() => Assert.IsTrue(saveButton.IsEnabledAsync().Result));

      await Task.Run(() => saveButton.ClickAsync());
    }
    public async Task CheckUpdatedCasedefandwit()

    {

      var home = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Home\")");
      await Task.Run(() => Assert.That(home.TextContentAsync().Result, Does.Contain("Home")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Home\")");

      var book = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("button:has-text(\"Book a Recording\")");
      await Task.Run(() => Assert.That(book.TextContentAsync().Result, Does.Contain("Book a Recording")));
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Book a Recording\")");

      var court = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator("[aria-label=\"Select\\ Court\"]");
      await Task.Run(() => Assert.That(court.TextContentAsync().Result, Does.Contain("Court Name")));
      await Page.Frame("fullscreen-app-host").ClickAsync("[aria-label=\"Select\\ Court\"]");

      await Page.Frame("fullscreen-app-host")
        .ClickAsync($"[aria-label=\"Select\\ Court\\ items\"] div:has-text(\"{stringCourt}\")");

      var caseInput = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host")
        .Locator("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Task.Run(() => Assert.IsTrue(caseInput.IsVisibleAsync().Result));
      await Page.IsVisibleAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");

      await Page.Frame("fullscreen-app-host").ClickAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]");
      await Page.Frame("fullscreen-app-host")
        .FillAsync("[placeholder=\"Case\\ Number\\ \\\\\\ URN\"]", $"{stringCase}");
        await Page.Frame("fullscreen-app-host").ClickAsync(".container_1f0sgyp div:nth-child(2) .react-knockout-control .appmagic-svg");
      await Page.Frame("fullscreen-app-host").ClickAsync("button:has-text(\"Modify\")");

      var defBox = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "[aria-label=\"Enter\\ your\\ Defendants\\,\\ comma\\ seperated\"]");
      await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Contain("Updatedef 1")));
      await Task.Run(() => Assert.That(defBox.InputValueAsync().Result, Does.Contain("Updatedef 2")));

      var WitnessBox = UpdateBookedRecordings._pagesetters.Page.Frame("fullscreen-app-host").Locator(
        "[aria-label=\"Enter\\ your\\ Witnesses\\,\\ comma\\ seperated\"]");
      await Task.Run(() => Assert.That(WitnessBox.InputValueAsync().Result, Does.Contain("UpdateWitness 1")));
      await Task.Run(() => Assert.That(WitnessBox.InputValueAsync().Result, Does.Contain("UpdateWitness 2")));

    }
   }
}
