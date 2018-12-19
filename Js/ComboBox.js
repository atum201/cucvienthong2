function combobox_initialize(comboboxID)
{
	var DROPDOWN_DOWN_ARROW_WIDTH = 18;

	var combobox = document.getElementById(comboboxID + '_container');
	var combobox_textbox = document.getElementById(comboboxID + '');
	var combobox_dropdown = document.getElementById(comboboxID + '_dropdown');

	var controlWidth = combobox_dropdown.offsetWidth;
	var controlHeight = combobox_dropdown.offsetHeight;
	var visibleWidth = controlWidth - DROPDOWN_DOWN_ARROW_WIDTH;
	var visibleHeight = controlHeight;

	combobox_textbox.offsetParent.style.width = (controlWidth) + 'px';

	combobox.style.width = (controlWidth) + 'px';
	combobox_textbox.style.width = (visibleWidth + 1) + 'px';
	combobox_textbox.style.height = visibleHeight + 'px';
	combobox_textbox.style.visibility = 'visible';

	var szClippingRegion = 'rect (auto auto auto ' + (visibleWidth - 1) + 'px)';
	combobox_dropdown.style.overflow = 'hidden';
	combobox_dropdown.style.clip = szClippingRegion;
	combobox_dropdown.selectedIndex = -1;
	combobox_dropdown.style.visibility = 'visible';

	// browser specific hacks
	if(document.all)
	{
		combobox_dropdown.style.marginTop = '1px';
	}
	else
	{
		combobox.style.verticalAlign = '-6px;'
		combobox_textbox.style.paddingLeft = '3px';
		combobox_textbox.style.backgroundColor = 'white';
	}
}

function combobox_dropdown_onchange(comboboxID)
{
	var combobox_textbox = document.getElementById(comboboxID + '');
	var combobox_dropdown = document.getElementById(comboboxID + '_dropdown');
	combobox_textbox.value = combobox_dropdown[combobox_dropdown.selectedIndex].text;
}

function combobox_textbox_onkeydown(comboboxID, e)
{
	var combobox_textbox = document.getElementById(comboboxID + '');
	var combobox_dropdown = document.getElementById(comboboxID + '_dropdown');
	combobox_dropdown.selectedIndex = -1;
	return true;
}

function combobox_textbox_autofill_onkeyup(comboboxID, e)
{
	var charCode = (e.charCode ? e.charCode : e.keyCode);
	if(charCode < 32 || (charCode >= 33 && charCode <= 46) || (charCode >= 112 && charCode <= 123))
	{
		return true;
	}

	var combobox_textbox = document.getElementById(comboboxID + '');
	var combobox_dropdown = document.getElementById(comboboxID + '_dropdown');

	var searchString = combobox_textbox.value.toUpperCase();
	var currentLength = searchString.length;

	for(i=0;i<combobox_dropdown.options.length;i++)
	{
		var option = combobox_dropdown.options[i];
		var optionText = option.text.toUpperCase();
		if(optionText.indexOf(searchString) == 0)
		{
			var optionLength = option.text.length;
			combobox_textbox.value = option.text;
			combobox_selectrange(combobox_textbox, currentLength, optionLength - currentLength);
			return false;
			break;
		}
	}
	return true;
}

function combobox_selectrange(element, start, length)
{
	if(element.createTextRange)
	{
		var oRange = element.createTextRange();
		oRange.moveStart('character', start);
		oRange.moveEnd('character', length);
		oRange.select();
	}
	else if(element.setSelectionRange)
	{
		element.setSelectionRange(start, start + length);
	}

	element.focus();
};
