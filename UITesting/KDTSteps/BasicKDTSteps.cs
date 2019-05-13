using System;
using NCalc;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using UITesting.Framework.Core;
using UITesting.Framework.UI;
using UITesting.Framework.UI.Controls;
using UITesting.Pages.Banking;
using TechTalk.SpecFlow;

namespace UITesting.KDTSteps
{
	[Binding]
	public class BasicKDTSteps
	{
		public BasicKDTSteps()
		{
		}
		[Before]
		public void SetUp()
		{
			DesiredCapabilities capabilities = new DesiredCapabilities();
			Driver.Add(Configuration.Platform, Configuration.DriverPath, capabilities);
			Context.ClearCurrent();
		}
		[After]
		public void TearDown()
		{
			Driver.Quit();
		}
		[Given("^the banking application has been started$")]
		public void StartBankingApplication()
		{
			Driver.Current().Navigate().GoToUrl("http://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");
		}

		[Given("^I am on the \"(.*)\" (?:page|screen)$")]
		[When("^(?:I |)go to the \"(.*)\" (?:page|screen)$")]
		public void NavigateToPage(String name)
		{
			Page target = Page.Screen(name);
			Assert.NotNull(target, "Unable to find the '" + name + "' page.");
			target.Navigate();
			VerifyCurrentPage(name);
		}

		[When("^(?:I |)(?:click|tap) on the \"(.*)\" (?:button|element|control)$")]
		public void ClickOnTheButton(String name)
		{
			Control control = Page.Current[name];
			Assert.NotNull(control, "Unable to find the '" + name + "' element on current page.");
			control.Click();
		}

		[Then("^I should see the \"(.*)\" (?:page|screen)$")]
		public void VerifyCurrentPage(String name)
		{
			Page target = Page.Screen(name);
			Assert.True(target.IsCurrent(), "The '" + name + "' screen is not current");
			Page.Current = target;
		}

		[Then("^(?:I should see |)the \"(.*)\" field is available$")]
		public Control VerifyElementExists(String fieldName)
		{
			Control control = Page.Current[fieldName];
			Assert.NotNull(control, "Unable to find the '" + fieldName + "' element on current page.");
			return control;
		}

		[When("^(?:I |)enter \"(.*)\" text into the \"(.*)\" field$")]
		public void EnterText(String text, String fieldName)
		{
			Edit control = (Edit)VerifyElementExists(fieldName);
			control.Text = text;
		}

		[Then("^(?:I should see |)the \"(.*)\" field contains the \"(.*)\" text$")]
		public void VerifyFieldText(String fieldName, String text)
		{
			Control control = (Control)VerifyElementExists(fieldName);
			String actualText = control.Text;
			Assert.True(
				text.Equals(actualText) || actualText.Contains(text),
				String.Format("The '{0}' field has unexpected text. Expected: '{1}', Actual: '{2}'",
				fieldName,
				text,
				actualText
			));
		}

		[When("^(?:I |)accept the alert message$")]
		public void AcceptAlert()
		{
			Driver.Current().SwitchTo().Alert().Accept();
		}

		[Then("^(?:I should see |)the \"(.*)\" text is shown$")]
		public void VerifyTextPresent(String text)
		{
			Assert.True(
				Page.Current.IsTextPresent(text),
				"Unable to find text: '" + text + "'");
		}

		[Then("^(?:I should see |)the following fields are shown:$")]
		public void VerifyMultipleFieldsAvailability(Table data)
		{
			int count = data.RowCount;
			for (int i = 0; i < count; i++)
			{
				VerifyElementExists(data.Rows[i]["Field"]);
			}
		}

		[Then("^(?:I should see |)the following labels are shown:$")]
		public void VerifyMultipleLabelsAvailability(Table data)
		{
			int count = data.RowCount;
			for (int i = 0; i < count; i++)
			{
				VerifyTextPresent(data.Rows[i]["Label"]);
			}
		}

		[When("^(?:I |)populate current page with the following data:$")]
		public void populatePageWithData(Table data)
		{
			int count = data.RowCount;
			for (int i = 0; i < count; i++)
			{
				EnterText(data.Rows[i]["Value"], data.Rows[i]["Field"]);
			}
		}

		[Then("^(?:I should see |)the page contains the following data:$")]
		public void pageContainsData(Table data)
		{
			int count = data.RowCount;
			for (int i = 0; i < count; i++)
			{
				VerifyFieldText(data.Rows[i]["Field"], data.Rows[i]["Value"]);
			}
		}
	    [Then("^(?:I should see |)the \"(.*)\" (?:list|table) is not empty$")]
		public void verifyListEmptyState(String list)
		{
			TableView control = (TableView) VerifyElementExists(list);
 			Assert.True(control.IsNotEmpty(), "The '" + list + "' element is empty");
		}

		[Then("^(?:I should see |)the (first|last) (?:row|item) of the \"(.*)\" (?:list|table) contains the following data:$")]
		public void verifyListRowData(String firstLast, String list, Table data)
		{
			int index = 0;
			TableView control = (TableView)VerifyElementExists(list);
			if (firstLast.Equals("last"))
			{
				index = control.ItemsCount - 1;
			}
			int count = data.RowCount;
			for (int i = 0; i < count; i++)
			{
				foreach (String key in data.Rows[i].Keys)
				{
					String value = data.Rows[i][key];
					Assert.AreEqual(value, control[key, index].Text,
									String.Format("The {0} row element '{1}' has unexpected value", firstLast, key)
								   );
				}
			}
		}

		[When("^(?:I |)(?:click|tap) on the (first|last) \"(.*)\" element of the \"(.*)\" (?:list|table)$")]
		public void clickOnSubItem(String firstLast, String item, String list)
		{
	        int index = 0;
			TableView control = (TableView) VerifyElementExists(list);
	        if (firstLast.Equals("last")) 
			{
				index = control.ItemsCount - 1;
			}
			control[item, index].Click();
		}
	    [When("^(?:I |)note the \"(.*)\" (?:table|list) (?:row|item) count as \"(.*)\"")]
		public void NoteRowCountAs(String list, String varName)
		{
			TableView control = (TableView) VerifyElementExists(list);
			Context.Put(varName, control.ItemsCount);
		}

		[When("^(?:I |)note the \"(.*)\" field text as \"(.*)\"")]
		public void NoteControlTextAs(String list, String varName)
		{
			Control control = VerifyElementExists(list);
			Context.Put(varName, control.Text);
		}
		[Then("^(?:I should see |)the \"(.*)\" (?:table|list) has \"(.*)\" (?:items|rows)$")]
		public void VerifyTableRowCount(String list, String countValue)
		{
			TableView control = (TableView) VerifyElementExists(list);

			double actualCount = Double.Parse(control.ItemsCount.ToString());
			String expectedCountValue = countValue;
	        foreach (String key in Context.Variables) 
			{
	            expectedCountValue = expectedCountValue.Replace(key, Context.Get(key).ToString());
	        }
			Expression expression = new Expression(expectedCountValue);
			double expectedCount = Double.Parse(expression.Evaluate().ToString());
			Assert.AreEqual(expectedCount, actualCount, 0.0001,
			               "Unexpected row count for the '" + list + "' table");
    	}

	    [Given("^I am logged as the \"(.*)\" customer$")]
		public void LoginAsCustomer(String name)
		{
    	    ((HomePage) Page.Screen("Banking Home")).LoginAsCustomer(name);
		}
	    [Then("^(?:I should see |)the \"(.*?)\" field value is calculated using the following formula:$")]
		public void FieldValueIsCalculatedByFormula(String field, String formula)
		{
			double precision = 0.0099;
			double actualCount = Double.Parse(Page.Current[field].Text);
			String expectedCountValue = formula;
			foreach (String key in Context.Variables)
			{
				expectedCountValue = expectedCountValue.Replace(key, Context.Get(key).ToString());
			}
			Expression expression = new Expression(expectedCountValue);
			double expectedCount = Double.Parse(expression.Evaluate().ToString());
			Assert.AreEqual(expectedCount, actualCount, precision,
			                "Wrong " + field + "! on page (" + actualCount
                			+ ") vs calulated (" + expectedCount + ")");
    }
	}
}
