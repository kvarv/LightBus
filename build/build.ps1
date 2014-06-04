Framework "4.0"

properties {
	$base_dir = resolve-path .\..
	$build_dir = "$base_dir\build"
	$properties_dir = "$build_dir\properties"
	$source_dir = "$base_dir\src"
	$build_artifacts_dir = "$base_dir\build_artifacts"
	$tools_dir = "$base_dir\tools"
	$test_dir = "$build_artifacts_dir\tests"
	$global:build_configuration = "debug"
	$nuspec_file = "$build_artifacts_dir\LightBus.nuspec"
    $version = if ($env:APPVEYOR_BUILD_VERSION -ne $NULL) { $env:APPVEYOR_BUILD_VERSION } else { '0.0.0' }
    $assembly_version = $version -replace "\-.*$", ".0"
    $assembly_file_version = $version -replace "-[^0-9]*", "."
}

task default -depends compile, test

task ci -depends create_package

task mark_release {
    $global:build_configuration = "release"
}

task clean {
	Write-Host "Build version is: $env:APPVEYOR_BUILD_VERSION"
	Write-Host "Build version is: $env:APPVEYOR_BUILD_NUMBER"
	
	rd $build_artifacts_dir -recurse -force  -ErrorAction SilentlyContinue | out-null
	mkdir $build_artifacts_dir  -ErrorAction SilentlyContinue  | out-null
}

task compile -depends clean, restore_packages {
	exec { msbuild  $source_dir\LightBus.sln /t:Clean /t:Build /p:Configuration=$build_configuration /v:q /nologo }
}

task restore_packages {
	exec { & $tools_dir\nuget\nuget.exe restore $source_dir\LightBus.sln }	
}

task test {	
    exec { & $tools_dir\xunit\xunit.console.clr4.exe $test_dir\net40\$build_configuration\LightBus.Tests.dll /xml $test_dir\tests_results.xml }
}

task create_package -depends mark_release, create_common_assembly_info, compile, test, create_nuspec, reset_assembly_info -precondition { return $version -ne ''} {
	exec { & $tools_dir\nuget\nuget.exe pack $nuspec_file -OutputDirectory $build_artifacts_dir}
}

task reset_assembly_info {
	git checkout $source_dir\CommonAssemblyInfo.cs
}

task create_nuspec {
	$nuspec = "<?xml version=""1.0"" encoding=""utf-8""?>
<package xmlns=""http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd"">
    <metadata>
        <id>LightBus</id>
        <version>$version</version>
        <authors>Gøran Sveia Kvarv</authors>
        <owners>Gøran Sveia Kvarv</owners>
        <licenseUrl>http://www.apache.org/licenses/LICENSE-2.0</licenseUrl>
        <projectUrl>https://github.com/kvarv/LightBus</projectUrl>
        <requireLicenseAcceptance>false</requireLicenseAcceptance>
        <description>LightBus is a lightweight in-process bus.</description>
        <copyright>Gøran Sveia Kvarv</copyright>
        <tags>bus mediator event command query cqrs</tags>
    </metadata>   
    <files>
        <file src=""$build_artifacts_dir\LightBus\net40\$build_configuration\LightBus.dll"" target=""lib\net40""/>
    </files>
</package>" 

	$nuspec | out-file $nuspec_file -encoding utf8
}

task create_common_assembly_info {
    $commit = git log -1 --pretty=format:%H
	$date = Get-Date
	$asmInfo = "using System.Reflection;

[assembly: AssemblyVersionAttribute(""$assembly_version"")]
[assembly: AssemblyFileVersionAttribute(""$assembly_file_version"")]
[assembly: AssemblyCopyrightAttribute(""Copyright Gøran Sveia Kvarv 2011-" + $date.Year + """)]
[assembly: AssemblyProductAttribute(""LightBus"")]
[assembly: AssemblyTrademarkAttribute(""LightBus"")]
[assembly: AssemblyCompanyAttribute("""")]
[assembly: AssemblyConfigurationAttribute(""$build_configuration"")]
[assembly: AssemblyInformationalVersionAttribute(""$commit"")]"

	$asmInfo | out-file "$source_dir\CommonAssemblyInfo.cs" -encoding utf8    
}
