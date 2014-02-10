#Framework "4.0"

properties {
	$base_dir = resolve-path .\..
	$build_dir = "$base_dir\build"
	$configs_dir = "$build_dir\configs"
	$properties_dir = "$build_dir\properties"
	$source_dir = "$base_dir\src"
	$build_artifacts_dir = "$base_dir\build_artifacts"
	$tools_dir = "$base_dir\tools"
	$test_dir = "$build_artifacts_dir\tests"
}

task default -depends test

task compile -depends clean {
	exec { msbuild $source_dir\LightBus.sln /t:Build /p:Configuration=Release /v:q /nologo }
}

task clean {
	rd $build_artifacts_dir -recurse -force  -ErrorAction SilentlyContinue | out-null
	mkdir $build_artifacts_dir  -ErrorAction SilentlyContinue  | out-null
}

task test {	
	$testassemblies = get-childitem $test_dir -recurse -include *tests*.dll
	exec { & $tools_dir\xunit\xunit.console.exe $testassemblies /nologo /nodots /xml $test_dir\tests_results.xml }
}