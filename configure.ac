m4_define([v_maj], [0])
m4_define([v_min], [0])
m4_define([v_mic], [1])
m4_define([v_ver], [v_maj.v_min.v_mic])

m4_define([lt_cur], m4_eval(v_maj + v_min))
m4_define([lt_rev], v_mic)
m4_define([lt_age], v_min)

AC_INIT([gegueb-sharp], [v_ver], [enesim-devel@googlegroups.com])
AC_PREREQ([2.60])
AC_CONFIG_SRCDIR([configure.ac])
AC_CONFIG_MACRO_DIR([m4])
AC_CONFIG_HEADERS([config.h])

AM_INIT_AUTOMAKE([foreign 1.6 dist-bzip2])

case "$host" in
	*-*-mingw*|*-*-cygwin*)
		platform_win32=yes
		AC_DEFINE(PLATFORM_WIN32,1,[Platform is Win32])
		CC="gcc -mno-cygwin -g"
		HOST_CC="gcc"
		;;
	*)
		platform_win32=no
		;;
esac

AM_CONDITIONAL(PLATFORM_WIN32, test x$platform_win32 = xyes)

AC_CHECK_TOOL(CC, gcc, gcc)
AC_PROG_CC
AM_PROG_CC_STDC
AC_PROG_INSTALL

dnl may require a specific autoconf version
dnl AC_PROG_CC_FOR_BUILD
dnl CC_FOR_BUILD not automatically detected
CC_FOR_BUILD=$CC
BUILD_EXEEXT=
if test "x$cross_compiling" = "xyes"; then
	CC_FOR_BUILD=cc
	BUILD_EXEEXT=""
fi
AC_SUBST(CC_FOR_BUILD)
AC_SUBST(HOST_CC)
AC_SUBST(BUILD_EXEEXT)

# Set STDC_HEADERS
AC_HEADER_STDC
AC_LIBTOOL_WIN32_DLL
AM_PROG_LIBTOOL

if test x$platform_win32 = xyes; then
   # Get rid of 'cyg' prefixes in library names
   sed -e "s/\/cyg\//\/\//" libtool > libtool.new; mv libtool.new libtool; chmod 755 libtool
fi

# not 64 bit clean in cross-compile
AC_CHECK_SIZEOF(void *, 4)

CFLAGS='-g -Wall -Wunused -Wmissing-prototypes -Wmissing-declarations -Wstrict-prototypes  -Wmissing-prototypes -Wnested-externs  -Wshadow -Wpointer-arith -Wno-cast-qual -Wcast-align -Wwrite-strings'

if test "x$enable_maintainer_mode" = "xyes"; then
DEBUG_FLAGS='-debug'
else
DEBUG_FLAGS=
AC_ARG_ENABLE(debug, [  --enable-debug          Build debugger (.mdb) files for dlls],
	DEBUG_FLAGS='-debug'
)
fi

CSDEFINES=
CSFLAGS="$DEBUG_FLAGS $CSDEFINES"
AC_SUBST(CSFLAGS)

AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
	AC_MSG_ERROR([You need to install pkg-config])
fi

MONO_REQUIRED_VERSION=1.0
PKG_CHECK_MODULES(MONO_DEPENDENCY, mono >= $MONO_REQUIRED_VERSION, has_mono=true, has_mono=false)

AC_PATH_PROG(GACUTIL, gacutil, no)
if test "x$GACUTIL" = "xno" ; then
	AC_MSG_ERROR([No gacutil tool found. You need to install either the mono or .Net SDK.])
fi

AC_PATH_PROG(AL, al, no)
if test "x$AL" = "xno" ; then
	AC_MSG_ERROR([No al tool found. You need to install either the mono or .Net SDK.])
fi

if test "x$has_mono" = "xtrue"; then
GACUTIL_FLAGS='/package $(PACKAGE_VERSION) /gacdir $(DESTDIR)$(prefix)/lib'
GENERATED_SOURCES=generated/*.cs
AC_PATH_PROG(RUNTIME, mono, no)

# If mono is found, it's in the path.  Require it to be in the path at runtime as well
if test "x$RUNTIME" != "no" ; then
	RUNTIME=mono
fi

AC_PATH_PROG(CSC, mcs, no)
if test `uname -s` = "Darwin"; then
	LIB_PREFIX=
	LIB_SUFFIX=.dylib
else
	LIB_PREFIX=.so
	LIB_SUFFIX=
fi
else
AC_PATH_PROG(CSC, csc.exe, no)
GACUTIL_FLAGS=
GENERATED_SOURCES=generated\\\\*.cs
enable_dotnet=yes

if test x$CSC = "xno"; then
	AC_MSG_ERROR([You need to install either mono or .Net])
else
RUNTIME=
LIB_PREFIX=
LIB_SUFFIX=.dylib
fi
fi

CS="C#"
if test "x$CSC" = "xno" ; then
	AC_MSG_ERROR([No $CS compiler found])
fi

AC_SUBST(RUNTIME)
AC_SUBST(CSC)
AC_SUBST(GACUTIL)
AC_SUBST(GACUTIL_FLAGS)
AC_SUBST(LIB_PREFIX)
AC_SUBST(LIB_SUFFIX)
AC_SUBST(GENERATED_SOURCES)

AC_PATH_PROG(GAPI_PARSER, gapi2-parser, no)
AC_SUBST(GAPI_PARSER)
AC_PATH_PROG(GAPI_CODEGEN, gapi2-codegen, no)
AC_SUBST(GAPI_CODEGEN)
AC_PATH_PROG(GAPI_FIXUP, gapi2-fixup, no)
AC_SUBST(GAPI_FIXUP)

AC_SUBST(CFLAGS)

AC_OUTPUT([
Makefile
sources/Makefile
gegueb/Makefile
gegueb/gegueb-sharp.dll.config
gegueb/gegueb-sharp.pc
])

