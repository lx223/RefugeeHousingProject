# RefugeeHousingProject

A site which connects Greek homeowners who have appartments to rent with NGOs which are helping refugees to find places to live.

## Development Guide

### Prerequisites

* A C# and .NET development environment
* .NET MVC 4.6
* Local installation of MS SQL Server

### Setup

* Clone the repo
* Configure a Google Maps API key:
    * Get a Google Maps API key by doing one of the following:
    * Create a new key following [Google's API key instructions](https://developers.google.com/maps/documentation/javascript/get-api-key)
        * Softwire members can look up an existing dev API key in the shared password manager
        * Create an environment variable called `REFUGEE_HOUSING_GOOGLE_API_KEY`, whose value is your API key
    * Restart Visual Studio to pick up the environment variable change
* Open the solution in Visual Studio
* Make sure the `RefugeeHousing` project is the startup project
* Hit the Run button

### Adding text to the site

Any text visible to the user must be translatable, because the site is available in both English and Greek. This is handled by resource files (.resx) files.

To create some translated text:

* Add the English and Greek translation into the two resource files in the Resources folder (_el_ is the two letter code for Greek) under the same key.
* Refer to the resource in your views using `LocalizedText.<stringKey>`, e.g. `LocalizedText.ContactFormHeader`.

### Local email setup

You only need to do this if you want to test email delivery in your dev environment. If you do not set this up, the email will be written to the log file instead.

* Get a SendGrid API key by doing one of the following:
    * If you have a SendGrid account, generate a new API key with permissions for sending emails
    * Softwire users can look up an existing API key in the shared password manager
* Add an environment variable called `REFUGEE_HOUSING_SENDGRID_API_KEY`, whose value is your API key
* Restart Visual Studio to pick up the environment variable change
