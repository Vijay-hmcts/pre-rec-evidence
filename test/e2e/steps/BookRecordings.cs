using System.Threading.Tasks;
using TechTalk.SpecFlow;
using pre.test.pages;
using pre.test.Hooks;
using System;



namespace pre.test
{
  [Binding]
  public class BookRecordings
  {
    public static BookRecording _bookrecording;
    public static string use = "";
    public static PageSetters _pagesetters;

    public BookRecordings(PageSetters pageSetters)
    {
      _pagesetters = pageSetters;
      _bookrecording = new BookRecording(_pagesetters.Page);
    }


    [Given(@"all fields entered and click save")]
    public async Task Whenallfieldsenteredandclicksave()
    {
      use = "Case";
      await _bookrecording.EnterCaseDetails();
    }

    [Then(@"case will be created")]
    public async Task Thencasewillbecreated()
    {
      await _bookrecording.CheckCaseCreated();
    }

    [Given(@"i fill required data for creating recording")]
    public async Task Whenifillrequireddataforcreatingrecording()
    {
      use = "Schedule";
      await _bookrecording.EnterCaseDetails();
      await _bookrecording.ScheduleRecording();
    }

    [Then(@"schedules will be created")]
    public async Task Thenscheduleswillbecreated()
    {
      await _bookrecording.CheckCaseScheduled();
    }

    [Given(@"I select a court name")]
    public async Task WhenIselectacourtname()
    {
      await _bookrecording.SelectCourt();
    }

    [Then(@"I am presented only with MVP court names")]
    public async Task ThenIampresentedonlywithMVPcourtnames()
    {
      await _bookrecording.CheckCourt();
    }


    [Given(@"I select a date in the past")]
    public async Task WhenIselectadateinthepast()
    {
      use = "PastDate";
      await _bookrecording.EnterCaseDetails();
      await _bookrecording.selectPastDate();
    }


    [Then(@"an error message is displayed")]
    public async Task Thenanerrormessageisdisplayed()
    {
      await _bookrecording.pastDateErrorMessage();
    }


    [Then(@"the recordings box is filled")]
    public async Task Thentherecordingsboxisfilled()
    {
      await _bookrecording.checkRecordingBox();
    }


    [Given(@"i fill required data for creating ten recordings")]
    public async Task Givenifillrequireddataforcreatingtenrecordings()
    {
      use = "ScheduleTen";
      var orginalMonth = DateTime.UtcNow.ToString("MMM");
      await _bookrecording.EnterCaseDetails();
      for (int i = 0; i < _bookrecording.quotaNum; i++)
      {
        _bookrecording.day = (DateTime.UtcNow.AddDays(+i)).ToString("ddd");

        if (orginalMonth != (DateTime.UtcNow.AddDays(+i)).ToString("MMM") && (DateTime.UtcNow.AddDays(+i)).ToString("dd") == "01"){
            _bookrecording.changeMonthCount = _bookrecording.changeMonthCount + 1;
        }
          _bookrecording.month = (DateTime.UtcNow.AddDays(+i)).ToString("MMM");
          _bookrecording.dateNum = (DateTime.UtcNow.AddDays(+i)).ToString("dd");
          _bookrecording.year = (DateTime.UtcNow.AddDays(+i)).ToString("yyyy");
          await _bookrecording.ScheduleRecording();
      }
    }


    [Then(@"i can start recordings for the ten schedules")]
    public async Task Thenicanstartrecordingsforalltenschedules()
    {
      await _bookrecording.startTenRecordings();
    }





  }
}
