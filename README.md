# QnATranslatorSample
Sample of how to create a QnA Bot using Microsoft Translator API

This bot translates the user’s question to English, sends it to QnAMaker and then translates the answer back to the language that has detected for the user. 

Feel free to clone the repro and just update Web.config with you own credentials to give it a try

```cs
	<appSettings>
	<!-- update these with your BotId, Microsoft App Id and your Microsoft App Password-->
	<add key="BotId" value="" />
	<add key="MicrosoftAppId" value="" />
	<add key="MicrosoftAppPassword" value="" />
	<add key="QnASubscriptionKey" value="" />
	<add key="QnAKbId" value="" />
	<add key="TranslatorApiKey" value="" />
	</appSettings>
```
 
 
![alt text](http://i64.tinypic.com/10s4ro7.jpg)

![alt text](http://i68.tinypic.com/2gu9ims.jpg)
