#!/bin/sh
echo $MONO_OPTIONS
echo $@
mono '/Applications/Visual Studio.app/Contents/Resources/lib/monodevelop/bin/MSBuild/Current/bin/msbuild.dll' "$@"