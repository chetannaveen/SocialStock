# SocialStock : Design Document

Team Members: _**Chetan Naveen Vadde, Lokesh Chandra Gorrepati, Surya Siva Pranav Jakka, Sai Teja Golla, Tej**_

## Logo :

![dd doc](SocialStockLogo.jpg)




## **Introduction**

The goal of Social Stock is to give you the sentiment analysis of a stock on Twitter and Reddit along with stock metrics, so that a trader can take advantage of the situation and profit. We now understand how social media affects stock prices considering the [GameStop affair](https://www.nbcnews.com/business/business-news/gamestop-reddit-explainer-what-s-happening-stock-market-n1255922). 
With the help of our platform, a trader can obtain the following information about his preferred stocks:
*	Sentiment score/Sentiment indicator value of Twitter and Reddit
*	Fundamental data and stock price metric related data of his choice of companies

The above information will help the trader in decision making.


## **Story Board**

![Social Stock Homepage](SocialStockHomePage.jpg)

![Social Stock Dashboard view 1](socialstock_sentiment_page.png)

![Social Stock Dashboard view 2](socialstock_news_page.png)

### **Data Sources**
URL Formats: 

Data Source for Social media Sentiment Analysis: https://finnhub.io/api/v1/stock/social-sentiment?symbol={CompanySymbol}&from={FromDate}&to={ToDate}&token={Token}

Data Source for Stock metric Data: https://finnhub.io/api/v1/stock/metric?symbol={CompanySymbol}&metric=all&token={Token}

Data Source for News related to Company: https://finnhub.io/api/v1/company-news?symbol={CompanySymbol}&from={FromDate}&to={ToDate}&token={Token}

Data Source for News related to Company: https://api.social-searcher.com/v2/trends?q={CompanySymbol}&key={Key}&network=twitter

### Examples Of Data Feed related to AAPL(Apple)

[Data Source for Social media Sentiment Analysis](https://finnhub.io/api/v1/stock/social-sentiment?symbol=AAPL&from=2022-09-22&to=2022-10-22&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg)

[Data Source for Stock metric Data](https://finnhub.io/api/v1/stock/metric?symbol=AAPL&metric=all&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg)

[Data Source for News related to Company](https://finnhub.io/api/v1/company-news?symbol=AAPL&from=2022-09-01&to=2022-10-09&token=cd7l922ad3iasq2munj0cd7l922ad3iasq2munjg)

[Data Source for Trending Tweets related to Company](https://api.social-searcher.com/v2/trends?q=AAPL&key=84115b4028964b26ea46f08761beb279&network=twitter)

## **Functional Requirements**

### **Requirement 1.0: Get Sentiment indicator value**

### **Scenario**

As a user interested in observing social media sentiment for a company before making a trade decision, I want to know the sentiment of users on twitter and reddit in the last one month.

### **Dependencies**

The twitter sentiment data is available.

### **Assumptions**

User knows the stock symbol of the company.

### **Examples**

## 1.1 
**Given:** I want to find the sentiment data of the company "Microsoft"

**When:** I search for "MSFT"

**Then:** I should receive a single result with these attributes.

Twitter sentiment Data

* Twitter Mentions: 124
* Twitter Positive Mentions: 59
* Twitter Negative Mentions: 49
* Twitter Score: 0.03
* Twitter Positive Score: 0.94
* Twitter Negative Score: -0.95

Reddit sentiment Data

* Reddit Mentions: 3
* Reddit Negative Mentions: 2
* Reddit Positive Mentions: 0
* Reddit Score: -0.62
* Reddit Positive Score: 0.29
* Reddit Negative Score: -0.82



## 1.2 
**Given:** : I searched for some random term

**When:** I search for “abcdefghijkl”

**Then:** I should receive zero results (an empty list)


### **Requirement 2.0: Get Fundamental and stock price metrics**

### **Scenario**

As a user interested in Fundamental and Technical analysis, I want to know about my company fundamental data and stock price related metrics.

### **Dependencies**

Fundamental and stock price metric data is available.

### **Assumptions**

User knows the stock symbol of the company and has basic knowledge on fundamental metrics.

### **Examples**

## 2.1 
**Given:** I want to find the fundamental and price metrics of the company "Microsoft". 

**When:** I search for "MSFT"

**Then:** I should receive a single result with these attributes.

Stock price metric data

* The 10Day Average Trading Volume: 27.74402
* The 13Week Price Return Daily: -7.00568
* The 26Week Price Return Daily: -7.00568
* The 3Month Average Trading Volume: 548.1157
* The 52Week High: 349.65
* The52WeekLow: 219.18



## 2.2 
**Given:** I searched some random term

**When:** I search for some unknown term such as "abcdefghjik" 

**Then:** I should receive zero results (an empty list)

## **Scrum Roles**

- DevOps : Sai teja Boga
- Product Owner/Developer : Chetan Naveen Vadde
- Scrum Master: Lokesh Chandra Gorrepati
- Frontend Developer: Surya Siva Pranav Jakka
- Integration Developer: Sai Teja Golla

## **Weekly Meeting**

Saturday at 4 PM on Teams.
