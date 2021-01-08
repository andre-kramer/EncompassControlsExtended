using EllieMae.Encompass.Automation;
using EllieMae.Encompass.Forms;
using System;

namespace ControlsExtended
{
    static class Style
    {
        public static void Apply(Control control, string style, string value)
        {
            switch(style.ToLower())
            {
                case "background-attachment":
                    control.HTMLElement.style.backgroundAttachment = value;
                    break;
                case "background-image":
                    control.HTMLElement.style.backgroundImage = value;
                    break;
                case "background-position":
                    control.HTMLElement.style.backgroundPosition = value;
                    break;
                case "background-position-x":
                    control.HTMLElement.style.backgroundPositionX = value;
                    break;
                case "background-position-y":
                    control.HTMLElement.style.backgroundPositionY = value;
                    break;
                case "background-repeat":
                    control.HTMLElement.style.backgroundRepeat = value;
                    break;
                case "border-bottom":
                    control.HTMLElement.style.borderBottom = value;
                    break;
                case "border-bottom-color":
                    control.HTMLElement.style.borderBottomColor = value;
                    break;
                case "border-bottom-style":
                    control.HTMLElement.style.borderBottomStyle = value;
                    break;
                case "border-bottom-width":
                    control.HTMLElement.style.borderBottomWidth = value;
                    break;
                case "border-left":
                    control.HTMLElement.style.borderLeft = value;
                    break;
                case "border-left-color":
                    control.HTMLElement.style.borderLeftColor = value;
                    break;
                case "border-left-style":
                    control.HTMLElement.style.borderLeftStyle = value;
                    break;
                case "border-left-width":
                    control.HTMLElement.style.borderLeftWidth = value;
                    break;
                case "border-right":
                    control.HTMLElement.style.borderRight = value;
                    break;
                case "border-right-color":
                    control.HTMLElement.style.borderRightColor = value;
                    break;
                case "border-right-style":
                    control.HTMLElement.style.borderRightStyle = value;
                    break;
                case "border-right-width":
                    control.HTMLElement.style.borderRightWidth = value;
                    break;
                case "border-top":
                    control.HTMLElement.style.borderTop = value;
                    break;
                case "border-top-color":
                    control.HTMLElement.style.borderTopColor = value;
                    break;
                case "border-top-style":
                    control.HTMLElement.style.borderTopStyle = value;
                    break;
                case "border-top-width":
                    control.HTMLElement.style.borderTopWidth = value;
                    break;
                case "clear":
                    control.HTMLElement.style.clear = value;
                    break;
                case "clip":
                    control.HTMLElement.style.clip = value;
                    break;
                case "css-text":
                    control.HTMLElement.style.cssText = value;
                    break;
                case "cursor":
                    control.HTMLElement.style.cursor = value;
                    break;
                case "display":
                    control.HTMLElement.style.display = value;
                    break;
                case "filter":
                    control.HTMLElement.style.filter = value;
                    break;
                case "letter-spacing":
                    control.HTMLElement.style.letterSpacing = value;
                    break;
                case "line-height":
                    control.HTMLElement.style.lineHeight = value;
                    break;
                case "list-style":
                    control.HTMLElement.style.listStyle = value;
                    break;
                case "list-style-image":
                    control.HTMLElement.style.listStyleImage = value;
                    break;
                case "list-style-position":
                    control.HTMLElement.style.listStylePosition = value;
                    break;
                case "list-style-type":
                    control.HTMLElement.style.listStyleType = value;
                    break;
                case "margin":
                    control.HTMLElement.style.margin = value;
                    break;
                case "margin-bottom":
                    control.HTMLElement.style.marginBottom = value;
                    break;
                case "margin-left":
                    control.HTMLElement.style.marginLeft = value;
                    break;
                case "margin-right":
                    control.HTMLElement.style.marginRight = value;
                    break;
                case "margin-top":
                    control.HTMLElement.style.marginTop = value;
                    break;
                case "overflow":
                    control.HTMLElement.style.overflow = value;
                    break;
                case "padding":
                    control.HTMLElement.style.padding = value;
                    break;
                case "padding-bottom":
                    control.HTMLElement.style.paddingBottom = value;
                    break;
                case "padding-left":
                    control.HTMLElement.style.paddingLeft = value;
                    break;
                case "padding-right":
                    control.HTMLElement.style.paddingRight = value;
                    break;
                case "padding-top":
                    control.HTMLElement.style.paddingTop = value;
                    break;
                case "page-break-after":
                    control.HTMLElement.style.pageBreakAfter = value;
                    break;
                case "page-break-before":
                    control.HTMLElement.style.pageBreakBefore = value;
                    break;
                case "pixel-height":
                    try
                    {
                        control.HTMLElement.style.pixelHeight = Convert.ToInt32(value);
                    }
                    catch(Exception ex)
                    {
                        Macro.Alert(style + " value must be an integer value.");
                    }
                    break;
                case "pixel-left":
                    try
                    {
                        control.HTMLElement.style.pixelLeft = Convert.ToInt32(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be an integer value.");
                    }
                    break;
                case "pixel-top":
                    try
                    {
                        control.HTMLElement.style.pixelTop = Convert.ToInt32(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be an integer value.");
                    }
                    break;
                case "pixel-width":
                    try
                    {
                        control.HTMLElement.style.pixelWidth = Convert.ToInt32(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be an integer value.");
                    }
                    break;
                case "pos-height":
                    try
                    {
                        control.HTMLElement.style.posHeight = (float)Convert.ToDouble(value);
                    }
                    catch(Exception ex)
                    {
                        Macro.Alert(style + " value must be a numeric value.");
                    }
                    break;
                case "pos-left":
                    try
                    {
                        control.HTMLElement.style.posLeft = (float)Convert.ToDouble(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be a numeric value.");
                    }
                    break;
                case "pos-top":
                    try
                    {
                        control.HTMLElement.style.posTop = (float)Convert.ToDouble(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be a numeric value.");
                    }
                    break;
                case "pos-width":
                    try
                    {
                        control.HTMLElement.style.posWidth = (float)Convert.ToDouble(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be a numeric value.");
                    }
                    break;
                case "style-float":
                    control.HTMLElement.style.styleFloat = value;
                    break;
                case "text-align":
                    control.HTMLElement.style.textAlign = value;
                    break;
                case "text-decoration":
                    control.HTMLElement.style.textDecoration = value;
                    break;
                case "text-decoration-line-through":
                    try
                    {
                        control.HTMLElement.style.textDecorationLineThrough = Convert.ToBoolean(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be either true or false.");
                    }
                    break;
                case "text-decoration-none":
                    try
                    {
                        control.HTMLElement.style.textDecorationNone = Convert.ToBoolean(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be either true or false.");
                    }
                    break;
                case "text-decoration-overline":
                    try
                    {
                        control.HTMLElement.style.textDecorationOverline = Convert.ToBoolean(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be either true or false.");
                    }
                    break;
                case "text-decoration-underline":
                    try
                    {
                        control.HTMLElement.style.textDecorationUnderline = Convert.ToBoolean(value);
                    }
                    catch (Exception ex)
                    {
                        Macro.Alert(style + " value must be either true or false.");
                    }
                    break;
                case "text-indent":
                    control.HTMLElement.style.textIndent = value;
                    break;
                case "text-transform":
                    control.HTMLElement.style.textTransform = value;
                    break;
                case "vertical-align":
                    control.HTMLElement.style.verticalAlign = value;
                    break;
                case "white-space":
                    control.HTMLElement.style.whiteSpace = value;
                    break;
                case "word-spacing":
                    control.HTMLElement.style.wordSpacing = value;
                    break;
                case "z-index":
                    control.HTMLElement.style.zIndex = value;
                    break;
                default:
                    Macro.Alert("Style '" + style + "' was not found and could not be assigned.");
                    break;
            }
        }
    }
}
