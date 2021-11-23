package meta.log;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class DefaultLogService implements LogService {

    private static final Logger TRACE = LoggerFactory.getLogger("trace");
    private static final Logger DEBUG = LoggerFactory.getLogger("debug");
    private static final Logger INFO = LoggerFactory.getLogger("info");
    private static final Logger WARN = LoggerFactory.getLogger("warn");
    private static final Logger ERROR = LoggerFactory.getLogger("error");
    private static final Logger ACCESS = LoggerFactory.getLogger("accessLog");
    private static final Logger SERVICE_STATS_LOG = LoggerFactory.getLogger("serviceStatsLog");
    private static final Logger PROFILE = LoggerFactory.getLogger("profile");

    public void trace(String msg) {
        TRACE.trace(msg);
    }

    @Override
    public void trace(String format, Object... argArray) {
        TRACE.trace(format, argArray);
    }

    public void debug(String msg) {
        DEBUG.debug(msg);
    }

    public void debug(String format, Object... argArray) {
        DEBUG.debug(format, argArray);
    }

    public void debug(String msg, Throwable t) {
        DEBUG.debug(msg, t);
    }

    public void info(String msg) {
        INFO.info(msg);
    }

    public void info(String format, Object... argArray) {
        INFO.info(format, argArray);
    }

    public void info(String msg, Throwable t) {
        INFO.info(msg, t);
    }

    public void warn(String msg) {
        WARN.warn(msg);
    }

    public void warn(String format, Object... argArray) {
        WARN.warn(format, argArray);
    }

    public void warn(String msg, Throwable t) {
        WARN.warn(msg, t);
    }

    public void error(String msg) {
        ERROR.error(msg);
    }

    public void error(String format, Object... argArray) {
        ERROR.error(format, argArray);
    }

    public void error(String msg, Throwable t) {
        ERROR.error(msg, t);
    }

    public void accessLog(String msg) {
        ACCESS.info(msg);
    }

    public void accessStatsLog(String msg) {
        SERVICE_STATS_LOG.info(msg);
    }

    public void accessStatsLog(String format, Object... argArray) {
        SERVICE_STATS_LOG.info(format, argArray);
    }

    public void accessProfileLog(String format, Object... argArray) {
        PROFILE.info(format, argArray);
    }

    @Override
    public boolean isTraceEnabled() {
        return TRACE.isTraceEnabled();
    }

    @Override
    public boolean isDebugEnabled() {
        return DEBUG.isDebugEnabled();
    }

    @Override
    public boolean isInfoEnabled() {
        return INFO.isInfoEnabled();
    }

    @Override
    public boolean isWarnEnabled() {
        return WARN.isWarnEnabled();
    }

    @Override
    public boolean isErrorEnabled() {
        return ERROR.isErrorEnabled();
    }

    @Override
    public boolean isStatsEnabled() {
        return SERVICE_STATS_LOG.isInfoEnabled();
    }
}
