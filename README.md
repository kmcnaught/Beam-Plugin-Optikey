# Beam-Plugin-Optikey

A plugin that allows the Beam webcam-based eye tracking engine to be used with [Optikey](https://github.com/Optikey/Optikey/). Note that webcam-based eye trackers will give less accurate and more unstable eye gaze prediction that separate eye tracking hardware, and may not be suitable for all users. In particular, the tracking may be susceptible to interference from head movements. This plugin is provided for individual experimentation, but not expected to replace proprietary eye tracking hardware.

## How to set up the Beam eye tracker

1. Install Beam eye tracker
2. Run & calibrate Beam eye tracker
3. Turn on Gaming Extensions in Beam
4. Select the Beam eye tracker as an input in Optikey
5. Always run Beam _before_ launching Optikey

### More details - installing and setting up Beam

The [Beam eyetracker engine](https://beam.eyeware.tech/) is [available through Steam](https://store.steampowered.com/app/2375780/Beam_Eye_Tracker/) or for direct install via the [Beam website](https://beam.eyeware.tech/).

If you install via Steam, you can demo for a limited time before purchase. 

Either way, after installing the Beam eye tracker run it and Calibrate. In the Beam Settings (available from the system tray) you need to turn on **Gaming Extensions**. 

Note that the eye tracking overlay **does not work correctly** with a docked app such as Optikey, so if you want an overlay, you would be better to use the overlay built into Optikey (Management Console -> Visuals -> Gaze Indicator Style)

![screenshot of Beam options](https://github.com/user-attachments/assets/e644a6e9-d20a-4412-a6ed-c9ac9fd6a21a)

### More details - setting up Optikey

Install the latest version (4.1+) of Optikey (Pro/Mouse/Chat/Symbol) from [Optikey releases](https://github.com/OptiKey/OptiKey/releases) or via the [Optikey website](https://optikey.org/) which contains further information about the features of each app. 

Run Optikey. Right-click on the on-screen keyboard and select "Management Console (Settings)". Go to the "Pointing & Selecting" tab, and under "POINTING: Source" click "Find more eye tracker options online". This plugin should be listed as an option, which you can install and use as Optikey's input. 

![image](https://github.com/user-attachments/assets/62e7627f-152e-4150-86b8-d4f0ed5c74a3)
