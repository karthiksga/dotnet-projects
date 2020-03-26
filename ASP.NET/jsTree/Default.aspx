<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="scripts/jsTree/_lib/css.js"></script>
	<script type="text/javascript" src="scripts/jsTree/_lib/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="scripts/jsTree/_lib/jquery.listen.js"></script>
	
	<script type="text/javascript" src="scripts/jsTree/source/tree_component.js"></script>						
	<link rel="stylesheet" type="text/css" href="scripts/jsTree/source/tree_component.css" />	
	<script type="text/javascript" src="scripts/jsTree/sarissa.js"></script>
	<script type="text/javascript" src="scripts/jsTree/sarissa_ieemu_xpath.js"></script>
	<script type="text/javascript" src="scripts/jsTree/jquery.xslt.js"></script>
	<script type="text/javascript">
		$(function() {			
			tree1 = new tree_component();
			tree1.init($("#divMainLeftPane"), {
			    data: {
			        type: "xml_flat",
			        url: "Menu.xml"
			    },						
				ui:{
					dots        : true,     // BOOL - dots or no dots			        
			        animation   : 300,      // INT - duration of open/close animations in miliseconds
			        theme_path  : "scripts/jsTree/source/themes/",
			        theme_name  : "default"			        					
				}
			}); 
		 });		 	 
	</script>	 
</head>
<body style="background-color:Black">
    <form id="form1" runat="server">
    <div id="divMainLeftPane">
		<%--<ul>
		<li id="predef_1" class="open">
			<a href="#">Link 1</a>
			<ul>

				<li id="predef_2"><a href="#" style="background-image:url('../images/globe.png');">Link 2</a></li>
				<li id="predef_3"><a href="#">Link 3</a></li>
				<li id="predef_4"><a href="#">Link 4</a>
					<ul>
						<li id="predef_5"><a href="#">Link 5</a></li>
						<li id="predef_6"><a href="#">Link 6</a></li>

						<li id="predef_7"><a href="#">Link 7</a>
					</ul>
				</li>
				<li id="predef_8"><a href="#">Link 5</a></li>
				<li id="predef_9" class="closed"><a href="#">Link 6</a></li>
			</ul>
		</li>
		<li id="pesho"><a href="#">Second root node</a></li>
	</ul>--%>
    </div>
    </form>
</body>
</html>
