Framework "4.0"

properties {
	$base_dir = resolve-path .\..
	$build_dir = "$base_dir\build"
	$properties_dir = "$build_dir\properties"
	$source_dir = "$base_dir\src"
	$build_artifacts_dir = "$base_dir\build_artifacts"
	$tools_dir = "$base_dir\tools"
	$test_dir = "$build_artifacts_dir\tests"
	$global:build_configuration = "Debug"
	$nuspec_file = "$build_artifacts_dir\LightBus.nuspec"
	$version = "0.1.0"
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

task create_package -depends compile, test, create_nuspec {
	exec { & $source_dir\.nuget\nuget.exe pack $nuspec_file -OutputDirectory $build_artifacts_dir}
}

task create_nuspec {
	$nuspec = "<?xml version=""1.0"" encoding=""utf-8""?>
<package xmlns=""http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd"">
    <metadata>
        <id>LightBus</id>
        <version>$version</version>
        <authors>G�ran Sveia Kvarv</authors>
        <owners>G�ran Sveia Kvarv</owners>
        <licenseUrl>http://www.apache.org/licenses/LICENSE-2.0</licenseUrl>
        <projectUrl>https://github.com/kvarv/LightBus</projectUrl>
        <requireLicenseAcceptance>false</requireLicenseAcceptance>
        <description>LightBus is a lightweight in-process bus.</description>
        <copyright>G�ran Sveia Kvarv</copyright>
        <tags>Bus Mediator Event Command Query</tags>
    </metadata>   
    <files>
        <file src=""$build_artifacts_dir\LightBus\LightBus.dll"" target=""lib\net40""/>
    </files>
</package>" 

	$nuspec | out-file $nuspec_file -encoding utf8
}