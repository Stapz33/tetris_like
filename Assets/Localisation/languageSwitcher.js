#pragma strict

function switchLanguage(language : String)
{
	Lang.GetInstance().ToggleLanguages();
}