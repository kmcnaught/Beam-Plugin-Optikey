# Beam-Plugin-Optikey

A plugin that allows the Beam webcam-based eye tracking engine to be used with [Optikey](https://github.com/Optikey/Optikey/). Note that webcam-based eye trackers will give less accurate and more unstable eye gaze prediction that separate eye tracking hardware, and may not be suitable for all users. In particular, the tracking may be susceptible to interference from head movements. This plugin is provided for individual experimentation, but not expected to replace proprietary eye tracking hardware.

## How to set up the Beam eye tracker

1. Install Beam eye tracker
2. Run & calibrate Beam eye tracker
3. Turn on Gaming Extensions in Beam
4. Select the Beam eye tracker as an input in Optikey

### More details 

The [Beam eyetracker engine](https://beam.eyeware.tech/) is [available through Steam](https://store.steampowered.com/app/2375780/Beam_Eye_Tracker/) or for direct install via the [Beam website](https://beam.eyeware.tech/).

If you install via Steam, you can demo for a limited time before purchase. 

Either way, after installing the Beam eye tracker run it and Calibrate. In the Beam Settings (available from the system tray) you need to turn on **Gaming Extensions**. 

Note that the eye tracking overlay does not work correctly with a docked app such as Optikey, so if you want an overlay, you would be better to use the overlay built into Optikey (Management Console -> Visuals -> Gaze Indicator Style)

![screenshot of Beam options](https://github.com/user-attachments/assets/e644a6e9-d20a-4412-a6ed-c9ac9fd6a21a)
