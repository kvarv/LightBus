Framework "4.0"

properties {
	$base_dir = resolve-path .\..
	$build_dir = "$base_dir\build"
	$configs_dir = "$build_dir\configs"
	$properties_dir = "$build_dir\properties"
	$source_dir = "$base_dir\src"
	$build_artifacts_dir = "$base_dir\build_artifacts"
	$tools_dir = "$base_dir\tools"
	$test_dir = "$build_artifacts_dir\tests"
	$global:build_configuration = "Debug"
}

task default -depends compile, test

task release {
    $global:build_configuration = "release"
}

task clean {
	rd $build_artifacts_dir -recurse -force  -ErrorAction SilentlyContinue | out-null
	mkdir $build_artifacts_dir  -ErrorAction SilentlyContinue  | out-null
}

task compile -depends clean {
	exec { msbuild  $source_dir\LightBus.sln /t:Clean /t:Build /p:Configuration=$build_configuration /v:q /nologo }
}

task test {	
	$testassemblies = get-childitem $test_dir -recurse -include *tests*.dll
	exec { & $tools_dir\xunit\xunit.console.clr4.exe $testassemblies /xml $test_dir\tests_results.xml }
}