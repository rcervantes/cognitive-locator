## Cognitive Locator

If you want to use the deploy to Azure feature go directly to the GitHub repo and click this button:
[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

The Cognitive Locator project, known publicly as 'Busca.me', is a project dedicated to reporting and finding missing persons. The project was founded due to the disasters related to the earthquake of September 19, 2017, which affected multiple states in Mexico. At first, the project was solely focused on finding or reporting people who went missing as a result of the earthquake. However, now that the project has grown, it not only aims to support the people affected at that time, but to anyone who is going through this devastating situation.

Cognitive Locator is a project hoping that every person can potentially be a medium to report and locate people who have been publicly declared as 'missing'.

The source code of the Cognitive Locator project is under the MIT license, nonetheless, the ‘Busca.me’ team is in charge of the implementation of the project.

This project is non-profit, seeing how it only supports non-governmental organizations that face different cases of disappearances every day.

## Architecture

Cognitive Locator is based on Xamarin Forms and Azure platform: Azure Functions, CosmosDB, Storage, Notification Hub, Cognitive Services (Face API).

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-architecture.png?raw=true" width="500" />
</div>

By uploading data for this demo, you agree that Microsoft may store it and use it to improve Microsoft services, including this API. To help protect your privacy, we take steps to de-identify your data and keep it secure. We won’t publish your data or let other people use it.

## Setup project

**Clone the project from GitHub repo**

`git clone https://github.com/robece/cognitive-locator.git`

**Click on the button Deploy to Azure to configure and recreate the Azure resources**

<div style="text-align:center">
	<img src="https://azuredeploy.net/deploybutton.png" />
</div>

Parameters:

- **Directory:** name of the directory that hosts your subscription.
- **Subscription:** name of the subscription you want to perform the deployment.
- **Resource Group:** confirm to create a new resource group.
- **Resource Group Name:** name of the resource group you want to use.
- **Site Location:** name of the location of the resource group and all the resources.
- **Storage Account Type:** type of storage: locally redundant storage (LRS), geo-redundant storage (GRS) or read-access geo-redundant storage (RA-GRS).
- **Database Consistency Level:** session is default, but the CosmosDB parameters are: eventual, strong, session, boundedstaleness.
- **Database Max Staleness Prefix:** 10 is default.
- **Database Max Interval In Seconds:** 5 is default.
- **Face Api Pricing Tier:** S0 is default, F0 (20 calls per minute, 30K calls per month) or S0 (10 calls per second).

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-azure-deploy.png?raw=true" width="500" />
</div>

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-azure-deploy2.png?raw=true" width="500" />
</div>

After create the resources you can validate it in your portal, e.g.:

- **Resource Group:** cognitive-locatora037
- **Azure Service Plan:** cognitive-locatora037-asp
- **Azure Database:** cognitive-locatora037-cos
- **Face API:** cognitive-locatora037-fac
- **Azure Function:** cognitive-locatora037-fun
- **Azure Notification Namespace:** cognitive-locatora037-ns
- **Azure Notification Hub:** cognitive-locatora037-hub
- **Storage:** (unique-identifier)stg

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-resources.png?raw=true" width="500" />
</div>

At this point you have to ways to configure the backend: **using a shell script** or **manually**.

**Using a shell script**

To execute the automation there are two prerequisites:

1. install <a href="https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest" target="_blank">Azure CLI</a>.

2. install <a href="https://stedolan.github.io/jq/download/" target="_blank">JQ</a>.

3. open a terminal go to the root of your repository: cognitive-locator, then write: chmod 700 azureconfig.sh, this allow us to run the script.

4. run the script: ./azureconfig.sh.

5. open a navigation tab and paste the authentication code provided in the url: `https://aka.ms/devicelogin`.

6. select the id of the subscription you have previously deployed the resources.

7. validate the correct subscription and type yes.

8. paste the name of the resource group name you did the deploy.

9. now, the database has been configured and will prompt the region for Face API, use the same as you selected in the One-Click-Deploy site location.

10. once, the database and face api has been configured you will need to add the cryptography key, the app center key for android and ios and you will finish the automation configuration.

11. since notification hub configuration is not available yet in Azure CLI you will manually need to add the missing configuration, go to Azure Function App application settings and configure the NotificationHub_Access_Signature setting.

12. the application uses Facebook Authentication you need to follow the steps in the Adding Facebook Authentication section provided in this document.

13. publish the azure function app to the cloud, all the settings needed are already configured.

**Manually**

1. go to your cosmosdb database account and create a database and database collection:
* database name: CognitiveLocator.
* database collection: Person.

2. configure the face api settings following the steps in the Configure Face API section provided in this document.

3. you need to configure all the application settings required in the azure function app:
* "AzureWebJobsStorage": "AZURE_STORAGE_CONNECTION_STRING",
* "AzureWebJobsDashboard": "AZURE_STORAGE_CONNECTION_STRING",
* "Face_API_Subscription_Key": "FACE_API_KEY",
* "Face_API_PersonGroupId": "missingpeople",
* "Face_API_Zone": "FACE_API_ZONE(e.g. westus or southcentralus)",
* "Face_API_FaceList": "list",
* "CosmosDB_URI": "COSMOSDB_URI",
* "CosmosDB_AuthKey": "COSMOSDB_KEY",
* "CosmosDB_DatabaseId": "CognitiveLocator",
* "CosmosDB_PersonCollection": "Person",
* "NotificationHub_Access_Signature": "NOTIFICATION_HUB_CONNECTION_STRING",
* "NotificationHub_Name": "NOTIFICTION_HUB_NAME",
* "Cryptography_Key": "CRYPT_KEY",
* "MobileCenterID_Android": "MOBILECENTER_ANDROID_APP_ID",
* "MobileCenterID_iOS": "MOBILECENTER_IOS_APP_ID",
* "ImageStorageUrl": "`https://YOUR_STORAGE_ACCOUNT.blob.core.windows.net/images/`"

4. the application uses Facebook Authentication you need to follow the steps in the Adding Facebook Authentication section provided in this document.

5. publish the azure function app to the cloud.

## Debugging locally

To debug locally the Function App you need to create a local.settings.json file in the Azure Functions project.

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "AZURE_STORAGE_CONNECTION_STRING",
    "AzureWebJobsDashboard": "AZURE_STORAGE_CONNECTION_STRING",
    "Face_API_Subscription_Key": "FACE_API_KEY",
    "Face_API_PersonGroupId": "missingpeople",
    "Face_API_Zone": "FACE_API_ZONE(e.g. westus or southcentralus)",
    "Face_API_FaceList": "list",
    "CosmosDB_URI": "COSMOSDB_URI",
    "CosmosDB_AuthKey": "COSMOSDB_KEY",
    "CosmosDB_DatabaseId": "CognitiveLocator",
    "CosmosDB_PersonCollection": "Person",
    "NotificationHub_Access_Signature": "NOTIFICATION_HUB_CONNECTION_STRING",
    "NotificationHub_Name": "NOTIFICTION_HUB_NAME",
    "Cryptography_Key": "CRYPT_KEY",
    "MobileCenterID_Android": "MOBILECENTER_ANDROID_APP_ID",
    "MobileCenterID_iOS": "MOBILECENTER_IOS_APP_ID",
    "ImageStorageUrl": "https://YOUR_STORAGE_ACCOUNT.blob.core.windows.net/images/"
  }
}
```

## Adding Facebook Authentication

Once we have created our Azure Functions and correctly deployed it's time to configure some settings to enable Facebook authentication, select Authentication/Authorization under Networking.

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-functions-settings.png?raw=true" width="800" />
</div>

Turn on the 'App Service Authentication' feature, then select 'Action to take when request is not authenticated' to Log in with Facebook.

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-functions-add-facebook.png?raw=true" width="500" />
</div>

Click on Facebook option then set the Facebook Application Id, Application Secret and the scope: public_profile and email, then save the configuration. 

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-functions-add-facebook-settings.png?raw=true" width="500" />
</div>

## Configure Facebook Application

Go to <a href="https://developer.facebook.com" target="_blank">Facebook Developer Portal</a> with your Facebook account add a new application e.g. Locator. now you will be able to see your Dashboard with your Application Name, Application ID and Application Secret.

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-facebook-dashboard.png?raw=true" width="500" />
</div>

Now it's time to configure the login, add the valid OAuth redirect URIs using your Azure Function callback URI previously configured: **`https://YOUR_AZURE_FUNCTION.azurewebsites.net/.auth/login/facebook/callback`** and verify the reset client OAuth settings.

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-facebook-login-settings.png?raw=true" width="800" />
</div>

Now go to Settings -> Basic and configure your Android and iOS App.

For Android. Fill the required fields and verify the rest of the settings, if you need a key hash, you can get it using <a href="https://blog.xamarin.com/simplified-android-keystore-signature-disovery/" target="_blank">this steps</a>.

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-facebook-android-setting.png?raw=true" width="500" />
</div>

For iOS. Fill the required fields and verify the rest of the settings.

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-facebook-ios-setting.png?raw=true" width="500" />
</div>

At this moment you have successfuly configure your Facebook application to login with your apps.

## Configure Face API

Now it's time to create our Face API 'Person Group' and 'Face List' in the specific API testing console:

**Person Group ID Parameters**
- personGroupId: missingpeople
- Content-Type: application/json
- Ocp-Apim-Subscription-Key: FACE_API_KEY
- Zone: Select the same zone where your FACE API has been created
- Post parameters:
```json
{
    "name":"Missing People",
    "userData":"Missing people person group"
}
```
- Link: <a href="https://southcentralus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395244" target="_blank">Person Group - Create a Person Group</a>

**Face List Parameters**
- faceListId: list
- Content-Type: application/json
- Ocp-Apim-Subscription-Key: FACE_API_KEY
- Zone: Select the same zone where your FACE API has been created
- Post parameters:
```json
{
    "name":"Face List",
    "userData":"Face List"
}
```
- Link: <a href="https://southcentralus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f3039524b" target="_blank">Face List - Create a Face List</a>

## Mobile application (Xamarin)

In your CognitiveLocator\App.xaml.cs file set the following attributes:

```csharp
    Settings.FunctionURL = "https://YOUR_AZURE_FUNCTION.azurewebsites.net";
    Settings.Cryptography = "YOUR_CRYPT_KEY"; //previously configured in the backend
```

In your CognitiveLocator.Droid project go to Resources\values\strings.xml and set the following attributes:

```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <string name="app_id">YOUR_FACEBOOK_APP_ID</string>
    <string name="app_name">YOUR_FACEBOOK_APP_NAME</string>
</resources>
```
In your CognitiveLocator.iOS project edit your Info.plist and set the following attributes:
For CFBundleURLSchemes just concatenate: fbXXXXXXXX your facebook app Id.

```xml
<key>CFBundleURLTypes</key>
	<array>
		<dict>
			<key>CFBundleURLSchemes</key>
			<array>
				<string>fbYOUR_FACEBOOK_APP_ID</string>
			</array>
		</dict>
	</array>
<key>FacebookAppID</key>
<string>YOUR_FACEBOOK_APP_ID</string>
<key>FacebookDisplayName</key>
<string>YOUR_FACEBOOK_APP_NAME</string>
```

Congrats if you successfully configure the app you can run it now!

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-app.png?raw=true" width="300" />
</div>

<div style="text-align:center">
	<img src="https://github.com/robece/cognitive-locator/blob/master/images/cognitive-locator-app2.png?raw=true" width="300" />
</div>

## Credits

I want to thank to <a href="https://github.com/robece/cognitive-locator/graphs/contributors" target="_blank">all contributors</a> who had participated in this project and those people who still continue participating actively on this project.
